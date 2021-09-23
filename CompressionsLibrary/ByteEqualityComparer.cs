using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CompressionsLibrary
{
    class ByteEqualityComparer : IEqualityComparer<byte[]>
    {
        public bool Equals([AllowNull] byte[] Uno, [AllowNull] byte[] Dos)
        {
            if (Uno.Length == Dos.Length)
            {
                for (int i = 0; i < Uno.Length; i++)
                {
                    if (Uno[i] != Dos[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetHashCode([DisallowNull] byte[] obj)
        {
            return StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
        }
    }
}
