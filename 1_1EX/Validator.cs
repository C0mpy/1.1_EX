﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using _1_1EX.Model;
using _1_1EX.WinTip;
using _1_1EX.WinEtiketa;

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
            foreach (Resurs r in MainWindow.resursi)
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

            for (int i = 0; i < MainWindow.tags.Count; i++)
            {
                if (MainWindow.tags.ElementAt(i).Id == s)
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
            for (int i = 0; i < MainWindow.types.Count; i++)
            {
                if (MainWindow.types.ElementAt(i).Id == s)
                {
                    return new ValidationResult(false, "Type with inputted ID exists.");
                }

            }

            return new ValidationResult(true, null);
        }
    }

    public class idTypeExistsManager : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string s = value as string;
            if (s == "")
            {
                return new ValidationResult(false, "Please input an ID.");
            }
            for (int i = 0; i < TypeManagement.types.Count; i++)
            {
                if (TypeManagement.types.ElementAt(i).Id == s)
                {
                    return new ValidationResult(false, "Type with inputted ID exists.");
                }

            }

            return new ValidationResult(true, null);
        }
    }

    public class idTagExistsManager : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (TagManagement.tags.Count != 0)
            {
                string s = value as string;
                if (s == "")
                {
                    return new ValidationResult(false, "Please input an ID.");
                }

                for (int i = 0; i < TagManagement.tags.Count; i++)
                {
                    if (TagManagement.tags.ElementAt(i).Id == s)
                    {
                        return new ValidationResult(false, "Tag with inputted ID exists.");
                    }
                }

            }
            return new ValidationResult(true, null);
        }
            
    }

}
