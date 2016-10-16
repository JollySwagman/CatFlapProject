using CatFlap.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Http;

namespace CatFlap.Controllers
{
    public class CatFlapController : ApiController
    {
        private string _logFile;

        public CatFlapController()
        {
            this._logFile = HttpContext.Current.Server.MapPath("~/App_Data/test.txt");
        }

        //public IHttpActionResult GetCatFlap(string id)
        //{
        //    Log(id);
        //    var cf = new Flap();
        //    return Ok(cf.Name);
        //}

        public IHttpActionResult GetCatFlap()
        {
            Log("Reading log");
            return Ok(File.ReadAllText(_logFile));
        }

        public IHttpActionResult PutCatFlap(string id)
        {
            Log(id);

            var consumerKey = "KPfjQDlmFn8X53NzNdDqJASYD";
            var consumerKeySecret = " REa2KO1JRcIST9hGbUopyFcIY4Sew3rcOfhYYJVZQxjmucDpNA";
            var accessToken = "274482112-urs2d4A9FPvZBQijwRMASCVMgOtYA1PSCfx9lqcy";
            var accessTokenSecret = " jhK0pKfsY46c2N8DWc1xBKQ2CjItX5B90Kjf0JSZbqwIc";


            var twit = new TwitterApi(consumerKey, consumerKeySecret, accessToken, accessTokenSecret);

            //var twitter = new TwitterApi(ConsumerKey, ConsumerKeySecret, AccessToken, AccessTokenSecret);
            var response = twit.Tweet("This is my first automated tweet!").Result;
            Trace.WriteLine(response);

//twit.Tweet("").


            var cf = new Flap();
            return Ok(cf.Name);
        }

        public IHttpActionResult PostCatFlap(string id)
        {
            var cf = new Flap();
            Log(id);
            return Ok(cf.Name);
        }

        private void Log(string message)
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            var AEST = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);

            File.AppendAllText(_logFile, AEST.ToString() + "--> " + message + Environment.NewLine);
        }
    }
}