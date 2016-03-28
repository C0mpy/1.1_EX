using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using _1_1EX.Model;

namespace _1_1EX
{
    public class StringToDoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
          
            string s = value as string;
            double r;
            if (s == "")
            {
                return new ValidationResult(false, "Please input a numerical value.");
            }
            else if (double.TryParse(s, out r))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Please input a numerical value.");
            }
            
               
        }
    }

    public class idExistsValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            string s = value as string;
            if (s == "")
            {
                return new ValidationResult(false, "Please input an ID.");
            }
            foreach(Resurs r in MainWindow.resursi) 
            {
                if (r.Id == s)
                {
                    return new ValidationResult(false, "Resurce with inputted ID exists.");
                }

            }

            return new ValidationResult(true, null);
        

        }
    }

    public class imePraznoValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            string s = value as string;
            if (s == "")
            {
                return new ValidationResult(false, "Please input a Name.");
            }
            return new ValidationResult(true, null);


        }
    }

    public class idTagExistsValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string s = value as string;
            if (s == "")
            {
                return new ValidationResult(false, "Please input an ID.");
            }
            foreach (Etiketa r in MainWindow.etikete)
            {
                if (r.Id == s)
                {
                    return new ValidationResult(false, "Tag with inputted ID exists.");
                }

            }

            return new ValidationResult(true, null);
        }
    }

    public class idTypeExistsValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string s = value as string;
            if (s == "")
            {
                return new ValidationResult(false, "Please input an ID.");
            }
            foreach (TipResursa r in MainWindow.tipovi)
            {
                if (r.Id == s)
                {
                    return new ValidationResult(false, "Type with inputted ID exists.");
                }

            }

            return new ValidationResult(true, null);
        }
    }

}
