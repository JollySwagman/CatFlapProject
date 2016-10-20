using CatFlap.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
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

        //public IHttpActionResult PutCatFlap(string id)
        //{

        //    Log(id);

        //    //var consumerKey = "KPfjQDlmFn8X53NzNdDqJASYD";
        //    //var consumerKeySecret = " REa2KO1JRcIST9hGbUopyFcIY4Sew3rcOfhYYJVZQxjmucDpNA";
        //    //var accessToken = "274482112-urs2d4A9FPvZBQijwRMASCVMgOtYA1PSCfx9lqcy";
        //    //var accessTokenSecret = " jhK0pKfsY46c2N8DWc1xBKQ2CjItX5B90Kjf0JSZbqwIc";

        //    //var twit = new TwitterApi(consumerKey, consumerKeySecret, accessToken, accessTokenSecret);

        //    ////var twitter = new TwitterApi(ConsumerKey, ConsumerKeySecret, AccessToken, AccessTokenSecret);
        //    //var response = twit.Tweet("This is my first automated tweet!").Result;
        //    //Trace.WriteLine(response);

        //    //twit.Tweet("").

        //    var cf = new Flap();
        //    return Ok("PutCatFlap(" + id + ") " + cf.Name);
        //}


        [System.Web.Http.HttpPost]
        public IHttpActionResult PostCatFlap(string payload, string hash)
        {

            const String SALT = "%8vvpAwg48cvlcRwfAiY%A4gEj"; // not very secure

            var hashCompare = Crypto.GetHashString(SALT + payload);


            //"fb9f3477b9d84e5b45dd48e0bf78c9cf21bf40""

            var cf = new Flap();

            var hashesOK = hashCompare.Equals(hash, StringComparison.CurrentCultureIgnoreCase);

            var x = string.Format("payload: [{0}], hash: [{1}], hashCompare: [{2}] AreEqual: {3}", payload, hash, hashCompare, hashesOK);

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine();

            return Ok("PostCatFlap() " + cf.Name + " " + x);
        }


        //public IHttpActionResult PostCatFlap(string id)
        //{
        //    var cf = new Flap();
        //    Log(id);
        //    return Ok("PostCatFlap(" + id + ") " + cf.Name);
        //}

        private void Log(string message)
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            var AEST = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);

            File.AppendAllText(_logFile, AEST.ToString() + "--> " + message + Environment.NewLine);
        }
    }
}
