using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
using System.Collections;

namespace Superstring
{
    class Program
    {
        /*
         http://www.geeksforgeeks.org/shortest-superstring-problem/
          
         
          Two strings are overlapping if prefix of one string is same suffix of other string or vice verse.
          The maximum overlap mean length of the matching prefix and suffix is maximum.
        
         arr[] = {"catgc", "ctaagt", "gcta", "ttca", "atgcatc"}
         Initialize:
         temp[] = {"catgc", "ctaagt", "gcta", "ttca", "atgcatc"}

        The most overlapping strings are "catgc" and "atgcatc"
        (Suffix of length 4 of "catgc" is same as prefix of "atgcatc")
        Replace two strings with "catgcatc", we get
        temp[] = {"catgcatc", "ctaagt", "gcta", "ttca"}

        The most overlapping strings are "ctaagt" and "gcta"
        (Prefix of length 3 of "ctaagt" is same as suffix of "gcta")
        Replace two strings with "gctaagt", we get
        temp[] = {"catgcatc", "gctaagt", "ttca"}

        The most overlapping strings are "catgcatc" and "ttca"
        (Prefix of length 2 of "catgcatc" as suffix of "ttca")
        Replace two strings with "ttcatgcatc", we get
        temp[] = {"ttcatgcatc", "gctaagt"}

        Now there are only two strings in temp[], after combing
        the two in optimal way, we get tem[] = {"gctaagttcatgcatc"}

        Since temp[] has only one string now, return it.
         */
        private static SuperStringSol _sol;
        static void Main(string[] args)
        {
            _sol = new SuperStringSol();

            string str1 = "catgcatc", str2 = "ttca";

            //Test Overlapping Index Function.
            //int i = 0, j = 0;
            //_sol.GetOverlapIndex(str1, str2, ref i, ref j);
            //WriteLine($"i value {i} , j value {j}");

            //Problem Solution.
            WriteLine(_sol.MergeOverlapedStrings(str1, str2));

            ReadKey();
        }
    }

    public class SuperStringSol
    {
        /// <summary>
        /// Printout Array elements.
        /// </summary>
        /// <param name="ar"></param>
        public void Print(ArrayList ar)
        {
            for (int i = 0; i < ar.Count; i++)
            {
                WriteLine(Convert.ToString(ar[i]));
            }
        }

        /// <summary>
        /// Shredder
        /// Split given string into chunks Both from RLT, LTR
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public ArrayList Shredder(string strInput)
        {
            ArrayList ar = new ArrayList();
            int iStartIndex = 0;
            int iCount = strInput.Length;

            //R-L
            for (int i = 0; i < iCount; i++)
            {
                string strTemp = strInput.Substring(iStartIndex, i + 1);
                ar.Add(strTemp);
            }
            strInput = ReverseString(strInput);

            //L-R
            for (int i = 0; i < iCount; i++)
            {
                string strTemp = ReverseString(strInput.Substring(iStartIndex, i + 1));
                ar.Add(strTemp);
            }

            ar = RemDuplication(ar);

            return ar;
        }

        /// <summary>
        /// Reverse given string s
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        /// <summary>
        /// Remove duplicated items from arraylist
        /// </summary>
        /// <param name="ar"></param>
        /// <returns></returns>
        public ArrayList RemDuplication(ArrayList ar)
        {
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < ar.Count; i++)
            {
                if (!arrayList.Contains(ar[i]))
                {
                    arrayList.Add(ar[i]);
                }
            }
            return arrayList;
        }

        /// <summary>
        /// Check if two arraylist are overlapping
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="len"></param>
        /// <param name="strOver"></param>
        /// <returns></returns>
        public bool IsOverloaped(ArrayList a, ArrayList b, ref int len, ref string strOver)
        {
            bool bResult = false;
            int OverLaopStringLen = 0;

            for (int i = 0; i < a.Count; i++)
            {
                string str1 = Convert.ToString(a[i]);

                for (int j = 0; j < b.Count; j++)
                {
                    string str2 = Convert.ToString(b[j]);

                    if (str1.Equals(str2) && str1.Length > OverLaopStringLen)
                    {
                        len = OverLaopStringLen = str1.Length;
                        strOver = str1;
                        bResult = true;
                    }
                }
            }

            return bResult;
        }

        /// <summary>
        /// Get Overlapping String from Given String ArrayLists
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public string GetOverlapingString(ArrayList a, ArrayList b)
        {
            string strResult = "";
            int OverLaopStringLen = 0;

            for (int i = 0; i < a.Count; i++)
            {
                string str1 = Convert.ToString(a[i]);

                for (int j = 0; j < b.Count; j++)
                {
                    string str2 = Convert.ToString(b[j]);

                    if (str1.Equals(str2) && str1.Length > OverLaopStringLen)
                    {
                        OverLaopStringLen = str1.Length;
                        strResult = str1;
                    }
                }
            }

            return strResult;
        }

        /// <summary>
        /// Get Refs of Overlapped from Both Strings.
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <param name="Indx1"></param>
        /// <param name="Indx2"></param>
        public void GetOverlapIndex(string str1, string str2, ref int Indx1, ref int Indx2)
        {
            string strOverlaped = GetOverlapingString(Shredder(str1), Shredder(str2));

            int iOver = 0;
            int iIter = 0;


            while (iOver != strOverlaped.Length)
            {
                if (str1[iIter] == strOverlaped[iOver])
                {
                    iIter++;
                    iOver++;
                }
                else
                {
                    iOver = 0;
                    Indx1 = -1;
                    iIter++;
                }

                if (iOver == strOverlaped.Length)
                {
                    Indx1 = iIter - strOverlaped.Length;
                    break;
                }
            }

            iIter = iOver = 0;

            while (iOver != strOverlaped.Length)
            {
                if (str2[iIter] == strOverlaped[iOver])
                {
                    iIter++;
                    iOver++;
                }
                else
                {
                    iOver = 0;
                    Indx2 = -1;
                    iIter++;
                }

                if (iOver == strOverlaped.Length)
                {
                    Indx2 = iIter - strOverlaped.Length;
                    break;
                }
            }

        }

        /// <summary>
        /// Get Refs of Overlapped from Both Strings and Overlapping string.
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <param name="Indx1"></param>
        /// <param name="Indx2"></param>
        public void GetOverlapIndexAndString(string str1, string str2, ref int Indx1, ref int Indx2, ref string strOver)
        {
            string strOverlaped = GetOverlapingString(Shredder(str1), Shredder(str2));

            if (strOverlaped.Length > 0)
                strOver = strOverlaped;


            int iOver = 0;
            int iIter = 0;


            while (iOver != strOverlaped.Length)
            {
                if (str1[iIter] == strOverlaped[iOver])
                {
                    iIter++;
                    iOver++;
                }
                else
                {
                    iOver = 0;
                    Indx1 = -1;
                    iIter++;
                }

                if (iOver == strOverlaped.Length)
                {
                    Indx1 = iIter - strOverlaped.Length;
                    break;
                }
            }

            iIter = iOver = 0;

            while (iOver != strOverlaped.Length)
            {
                if (str2[iIter] == strOverlaped[iOver])
                {
                    iIter++;
                    iOver++;
                }
                else
                {
                    iOver = 0;
                    Indx2 = -1;
                    iIter++;
                }

                if (iOver == strOverlaped.Length)
                {
                    Indx2 = iIter - strOverlaped.Length;
                    break;
                }
            }

        }

        /// <summary>
        /// Merge Overlapped Areas
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public string MergeOverlapedStrings(string str1, string str2)
        {
            string strOutput = "";
            string strOvr = string.Empty;
            int i = 0, j = 0;


            GetOverlapIndexAndString(str1, str2, ref i, ref j, ref strOvr);

            if (str1.Length > str2.Length)
            {
                if (j == 0)
                    strOutput = str2.Substring(0, 1);
                else
                    strOutput = str2.Substring(0, j);

                strOutput += strOvr;
                strOutput += str1.Substring(strOvr.Length, str1.Length - strOvr.Length);
            }

            if (str2.Length > str1.Length)
            {
                if (i == 0)
                    strOutput = str1.Substring(0, 1);
                else
                    strOutput = str1.Substring(0, i);

                strOutput += strOvr;
                strOutput += str2.Substring(strOvr.Length, str2.Length - strOvr.Length);
            }

            return strOutput;
        }

    }
}

//public void BestMatchPair(ArrayList ar, ref string str1, ref string str2, ref int iLen)
//{
//    if (ar.Count == 1 || ar.Count == 0)
//        return;

//    int iMaxLength = 0;

//    string strTemp1 = "";
//    string strTemp2 = "";

//    int i = 0;
//    strTemp1 = Convert.ToString(ar[i]);

//    for (int j = i + 1; j < ar.Count; j++)
//    {
//        strTemp2 = Convert.ToString(ar[j]);

//        int len = GetOverlapingString(Shredder(strTemp1), Shredder(strTemp2)).Length;

//        if (len > iMaxLength)
//        {
//            str1 = strTemp1;
//            str2 = strTemp2;
//            iMaxLength = len;
//            iLen = iMaxLength;
//        }
//    }
//}
/// <summary>
/// Remove duplicated items from arraylist
/// </summary>
/// <param name="ar"></param>
/// <returns></returns>
//public ArrayList RemDuplication(ArrayList ar)
//{
//    for (int i = 0; i < ar.Count; i++)
//    {
//        for (int j = i + 1; j < ar.Count; j++)
//        {
//            if (Convert.ToString(ar[i]).Equals(ar[j].ToString()))
//            {
//                ar.RemoveAt(j);
//                ar.TrimToSize();
//                j--;
//                break;
//            }
//        }
//    }
//    return ar;
//}

///// <summary>
///// Better Solution for Shredder
///// </summary>
///// <param name="strInput"></param>
///// <returns></returns>
//public ArrayList ShredderEx(string strInput)
//{
//    ArrayList ar = new ArrayList();
//    int iStartIndex = 0;
//    int iCount = strInput.Length;

//    while (iStartIndex != strInput.Length)
//    {
//        for (int i = 0; i < iCount; i++)
//        {
//            ar.Add(strInput.Substring(iStartIndex, i + 1));
//        }
//        iCount--;
//        iStartIndex++;
//    }


//    ar = RemDuplication(ar);

//    return ar;
//}


//public OverlapingType GetOverloapingType(string strInput, string strOver)
//{
//    OverlapingType c = OverlapingType.NON;

//    if (strInput[0] == strOver[0])
//    {
//        c = OverlapingType.Prefix;
//    }
//    else if (strInput[0] != strOver[0])
//    {
//        c = OverlapingType.Suffix;
//    }
//    return c;
//}


//public enum OverlapingType
//{
//    Prefix = 0,
//    Suffix = 1,
//    NON = -1
//};
