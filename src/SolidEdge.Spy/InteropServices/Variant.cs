using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SolidEdge.Spy.InteropServices
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Variant
    {
        [FieldOffset(0)]
        public ushort varType;
        [FieldOffset(2)]
        public ushort reserved1;
        [FieldOffset(4)]
        public ushort reserved2;
        [FieldOffset(6)]
        public ushort reserved3;
        [FieldOffset(8)]
        public short boolvalue;
        [FieldOffset(8)]
        public IntPtr ptr1;
        [FieldOffset(12)]
        public IntPtr ptr2;

        public Variant(VariantWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException("wrapper");

            varType = (ushort)(VarEnum.VT_VARIANT | VarEnum.VT_BYREF);
            reserved1 = 0;
            reserved2 = 0;
            reserved3 = 0;
            boolvalue = 0;
            ptr1 = IntPtr.Zero;
            ptr2 = IntPtr.Zero;

            // Allocate memory for the VT_BYREF VT_VARIANT.
            ptr1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Variant)));

            // Move the wrapped object into the memory.
            Marshal.GetNativeVariantForObject(wrapper.WrappedObject, ptr1);
        }

        public VarEnum VariantType
        {
            get
            {
                return (VarEnum)this.varType;
            }
            set
            {
                this.varType = (ushort)value;
            }
        }

        public object ToObject()
        {
            unsafe
            {
                fixed (Variant* p = &this)
                {
                    return Marshal.GetObjectForNativeVariant((IntPtr)p);
                }
            }
        }

        public void Clear()
        {
            VariantType = VarEnum.VT_EMPTY;

            unsafe
            {
                fixed (Variant* p = &this)
                {
                    NativeMethods.VariantClear((IntPtr)p);
                }
            }
        }
    }

    public class VariantPtr : SafeHandle
    {
        public VariantPtr(IntPtr p)
            : base(p, true)
        {
        }

        public static implicit operator IntPtr(VariantPtr p)
        {
            return p.handle;
        }

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                NativeMethods.VariantClear(this.handle);
            }
            catch
            {
            }

            return true;
        }
    }

    public class VariantArgPtr : SafeHandle
    {
        private List<IntPtr> _pVariants = new List<IntPtr>();

        public VariantArgPtr(int count)
            : base(IntPtr.Zero, true)
        {
            if (count > 0)
            {
                IntPtr p = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Variant)) * count);
                SetHandle(p);
            }

            for (int i = 0; i < count; i++)
            {
                _pVariants.Add(new IntPtr(this.handle.ToInt64() + (Marshal.SizeOf(typeof(Variant)) * i)));
            }
        }

        public IntPtr this[int i]
        {
            get
            {
                return _pVariants[i];
            }
        }

        public int Count { get { return _pVariants.Count; } }

        public static implicit operator IntPtr(VariantArgPtr p)
        {
            return p.handle;
        }

        #region Overrides

        public override bool IsInvalid
        {
            get { return this.handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            if (IsInvalid == false)
            {
                try
                {
                    foreach (IntPtr pVariant in _pVariants)
                    {
                        int hr = NativeMethods.VariantClear(pVariant);
                    }
                }
                catch
                {
                }

                try
                {
                    Marshal.FreeCoTaskMem(this.handle);
                }
                catch
                {
                }
                finally
                {
                    _pVariants.Clear();
                    SetHandle(IntPtr.Zero);
                }
            }

            return true;
        }
        #endregion

    }
}