using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    class LongNumber
    {
        private List<int> digits;

        public LongNumber()
        {
            digits = new List<int>();
            digits.Add(0);
        }

        public LongNumber(string number)
        {
            digits = new List<int>();
            for (int i = number.Length - 1; i >= 0; i--)
            {
                digits.Add(int.Parse(number[i].ToString()));
            }
        }

        public LongNumber(List<int> digits)
        {
            this.digits = digits;
        }

        public LongNumber(LongNumber other)
        {
            digits = new List<int>(other.digits);
        }

        public void AddDigit(int digit)
        {
            digits.Add(digit);
        }

        public void RemoveLeadingZeroes()
        {
            while (digits.Count > 1 && digits[digits.Count - 1] == 0)
            {
                digits.RemoveAt(digits.Count - 1);
            }
        }

        public static LongNumber operator +(LongNumber a, LongNumber b)
        {
            int carry = 0;
            List<int> result = new List<int>();
            for (int i = 0; i < Math.Max(a.digits.Count, b.digits.Count); i++)
            {
                int sum = carry;
                if (i < a.digits.Count)
                {
                    sum += a.digits[i];
                }
                if (i < b.digits.Count)
                {
                    sum += b.digits[i];
                }
                result.Add(sum % 10);
                carry = sum / 10;
            }
            if (carry > 0)
            {
                result.Add(carry);
            }
            return new LongNumber(result);
        }

        public static LongNumber operator -(LongNumber a, LongNumber b)
        {
            if (a < b)
            {
                throw new ArgumentException("a must be greater than or equal to b");
            }
            int borrow = 0;
            List<int> result = new List<int>();
            for (int i = 0; i < a.digits.Count; i++)
            {
                int difference = a.digits[i] - borrow;
                if (i < b.digits.Count)
                {
                    difference -= b.digits[i];
                }
                if (difference < 0)
                {
                    difference += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }
                result.Add(difference);
            }
            LongNumber differenceNumber = new LongNumber(result);
            differenceNumber.RemoveLeadingZeroes();
            return differenceNumber;
        }

        public static LongNumber operator *(LongNumber a, LongNumber b)
        {
            LongNumber product = new LongNumber();
            for (int i = 0; i < a.digits.Count; i++)
            {
                LongNumber row = new LongNumber();
                int carry = 0;
                for (int j = 0; j < b.digits.Count; j++)
                {
                    int digit = a.digits[i] * b.digits[j] + carry;
                    row.AddDigit(digit % 10);
                    carry = digit / 10;
                }
                if (carry > 0)
                {
                    row.AddDigit(carry);
                }
                for (int j = 0; j < i; j++)
                {
                    row.digits.Insert(0, 0);
                }
                product += row;
            }
            return product;
        }

        public static LongNumber operator /(LongNumber a, LongNumber b)
        {
            if (b.digits.Count == 1 && b.digits[0] == 0)
            {
                throw new DivideByZeroException();
            }
            LongNumber quotient = new LongNumber();
            LongNumber remainder = new LongNumber();
            for (int i = a.digits.Count - 1; i >= 0; i--)
            {
                remainder.digits.Insert(0, a.digits[i]);
                remainder.RemoveLeadingZeroes();
                int quotientDigit = 0;
                while (remainder >= b)
                {
                    remainder -= b;
                    quotientDigit++;
                }
                quotient.digits.Insert(0, quotientDigit);
            }
            quotient.RemoveLeadingZeroes();
            if (quotient.digits.Count == 0)
            {
                quotient.AddDigit(0);
            }
            return quotient;
        }

        public static LongNumber operator %(LongNumber a, LongNumber b)
        {
            if (b.digits.Count == 1 && b.digits[0] == 0)
            {
                throw new DivideByZeroException();
            }
            LongNumber remainder = new LongNumber();
            for (int i = a.digits.Count - 1; i >= 0; i--)
            {
                remainder.digits.Insert(0, a.digits[i]);
                remainder.RemoveLeadingZeroes();
                while (remainder >= b)
                {
                    remainder -= b;
                }
            }
            remainder.RemoveLeadingZeroes();
            if (remainder.digits.Count == 0)
            {
                remainder.AddDigit(0);
            }
            return remainder;
        }

        public static bool operator ==(LongNumber a, LongNumber b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(LongNumber a, LongNumber b)
        {
            return !a.Equals(b);
        }

        public static bool operator >(LongNumber a, LongNumber b)
        {
            if (a.digits.Count > b.digits.Count)
            {
                return true;
            }
            if (a.digits.Count < b.digits.Count)
            {
                return false;
            }
            for (int i = a.digits.Count - 1; i >= 0; i--)
            {
                if (a.digits[i] > b.digits[i])
                {
                    return true;
                }
                if (a.digits[i] < b.digits[i])
                {
                    return false;
                }
            }
            return false;
        }

        public static bool operator <(LongNumber a, LongNumber b)
        {
            if (a.digits.Count < b.digits.Count)
            {
                return true;
            }
            if (a.digits.Count > b.digits.Count)
            {
                return false;
            }
            for (int i = a.digits.Count - 1; i >= 0; i--)
            {
                if (a.digits[i] < b.digits[i])
                {
                    return true;
                }
                if (a.digits[i] > b.digits[i])
                {
                    return false;
                }
            }
            return false;
        }

        public static bool operator >=(LongNumber a, LongNumber b)
        {
            return a == b || a > b;
        }

        public static bool operator <=(LongNumber a, LongNumber b)
        {
            return a == b || a < b;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            LongNumber other = (LongNumber)obj;
            if (digits.Count != other.digits.Count)
            {
                return false;
            }
            for (int i = 0; i < digits.Count; i++)
            {
                if (digits[i] != other.digits[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            for (int i = 0; i < digits.Count; i++)
            {
                hashCode = 31 * hashCode + digits[i];
            }
            return hashCode;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                builder.Append(digits[i]);
            }
            return builder.ToString();
        }
    }
}
