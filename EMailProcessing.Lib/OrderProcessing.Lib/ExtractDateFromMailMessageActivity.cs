﻿using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderProcessing.Lib
{
    public class ExtractDateFromMailMessageActivity : CodeActivity
    {
        static readonly Regex rxDate = new Regex(@"(\d+) (\w{3}) (\d{4}) (\d+):(\d+):(\d+)", 
            RegexOptions.Compiled);
        static readonly List<string> months = new List<string>{ "Jan", "Feb", "Mar", "Apr", "May", "Jun",
            "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public static DateTime ParseDate(string sDate)
        {
            // 15 Mar 2019 09:36:49 -0400
            Match match = rxDate.Match(sDate);
            int day = int.Parse(match.Groups[1].Value),
                year = int.Parse(match.Groups[3].Value),
                hour = int.Parse(match.Groups[4].Value),
                min = int.Parse(match.Groups[5].Value),
                sec = int.Parse(match.Groups[6].Value),
                month = 1 + months.IndexOf(match.Groups[2].Value);
            return new DateTime(year, month, day, hour, min, sec);
        }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<MailMessage> EMail { get; set; }

        [Category("Output")]
        public OutArgument<DateTime> EMailDate { get; set; }
           

        protected override void Execute(CodeActivityContext context)
        {
            string sDate = EMail.Get(context).Headers["Date"];
            EMailDate.Set(context, ParseDate(sDate));

        }
    }
}
