using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NET_INIS4_PR2._2_z2
{
    public class Dane : INotifyPropertyChanged
    {
        bool
            checkSign = false,
            checkResult = false,
            checkPercent = false,
            checkSquer = false,
            notInt = false;

        double?
            first = null,
            second = null,
            result = 0;

        string 
            operation = null,
            lastSign = null,
            sFirst = null,
            sSecond = null;

        public string Result
        {
            get
            {
                if (checkPercent == true)
                    return Convert.ToString(result) + "%";
                else if (notInt == true)
                    return "Error";
                else
                    return Convert.ToString(result);
            }
            set
            {
                result = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }
        public string Operation
        {
            get
            {
                return Convert.ToString(operation);
            }
            set
            {
                operation = Convert.ToString(value);
                OnPropertyChanged();
            }
        }

        public void Number(string number)
        {
            if (checkResult != true && checkSquer != true)
            {
                if (first == null && checkSign == false)
                {
                    sFirst += number;
                    Result = sFirst;
                }
                else
                {
                    sSecond += number;
                    Result = sSecond;
                }

                Operation += number;
            }
        }
        public void WriteSign(string sign)
        {
            if (checkResult != true && checkSquer != true)
            {
                lastSign = sign;

                if (sign == "x^2")
                {
                    Operation += "^2";
                    first = Convert.ToDouble(sFirst);
                    DoOperation();
                    return;
                }
                else if (sign == "%")
                {
                    Operation += "*100%";
                    first = Convert.ToDouble(sFirst);
                    DoOperation();
                    return;
                }
                else if (checkResult == false && sign == "+/-")
                {
                    lastSign = sign;
                    first = Convert.ToDouble(sFirst);
                    DoOperation();
                }
                else if (sign == ",")
                {
                    Operation += ",";
                    sFirst += ",";
                    return;
                }
                else if (sign == "sqrt")
                {
                    Operation += sign;
                    first = Convert.ToDouble(sFirst);
                    DoOperation();
                    return;
                }
                else if (sign == "1/x")
                {
                    Operation = "1/" + sFirst;
                    first = Convert.ToDouble(sFirst);
                    DoOperation();
                    return;
                }
                else if (sign == "x!")
                {
                    Operation += "!";
                    first = Convert.ToDouble(sFirst);
                    DoOperation();
                    return;
                }
                else if (sign == "log")
                {
                    Operation += sign;
                    first = Convert.ToDouble(sFirst);
                    DoOperation();
                    return;
                }
                else if (sign == "rnd")
                {
                    Operation += sign;
                    first = Convert.ToDouble(sFirst);
                    DoOperation();
                    return;
                }


                if (checkSign == true && (sign != "x^2" || sign != "%" || sign != "+/-" || sign != "sqrt" || sign != "1/x" || sign != "x!"))
                    DoOperation();
                else
                {
                    first = Convert.ToDouble(sFirst);
                    checkSign = true;
                }

                Operation += sign;
                lastSign = sign;
            }
        }
        public void DoOperation()
        {
            second = Convert.ToDouble(sSecond);

            if (lastSign == "+")
                first += second;
            else if (lastSign == "-")
                first -= second;
            else if (lastSign == "*")
                first *= second;
            else if (lastSign == "/")
                first /= second;
            else if (lastSign == "x^2")
            {
                first *= first;
                checkSquer = true;
            }
            else if (lastSign == "%")
            {
                first *= 100;
                checkPercent = true;
            }
            else if (lastSign == "+/-")
                Result = Convert.ToString(-first);
            else if (lastSign == "sqrt")
            {
                first = Math.Sqrt(Convert.ToDouble(first));
                checkSquer = true;
            }
            else if (lastSign == "1/x")
            {
                first = 1 / first;
                checkSquer = true;
            }
            else if (lastSign == "x!")
            {
                if (first % 1 != 0)
                {
                    notInt = true;
                }

                double res = 1;

                for (double i = 1; i <= first; i++)
                    res *= i;

                first = res;
                checkSquer = true;
            }
            else if (lastSign == "mod")
                first %= second;
            else if (lastSign == "log")
            {
                first = Math.Log10(Convert.ToDouble(first));
                checkSquer = true;
            }
            else if (lastSign == "rnd")
            {
                first = Math.Round(Convert.ToDouble(first));
                checkSquer = true;
            }

            if (checkResult != true && lastSign != "+/-")
                Result = Convert.ToString(first);

            sSecond = null;
            lastSign = null;
        }
        public void ShowResult()
        {
            if (checkResult == false && checkPercent == false && checkSquer == false)
            {
                DoOperation();
                Operation += "=";
                Result = Convert.ToString(first);
                checkResult = true;
            }
            else 
                return;
        }
        public void Restart()
        {
            first = second = null;
            checkPercent = checkSquer = notInt = false;
            Result = Operation = sFirst = sSecond = null;
            checkSign = checkResult = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
