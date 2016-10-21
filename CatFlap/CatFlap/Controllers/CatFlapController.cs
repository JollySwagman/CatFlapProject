using CatFlap.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        /// <summary>
        /// Get all log entries
        /// </summary>
        /// <returns></returns>
        public List<Passage> Get()
        {
            Log("Reading log 2");
            return CatFlapData.GetAll().ToList();
        }

        //[System.Web.Http.HttpPost]
        /// <summary>
        /// Register a passage event
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public IHttpActionResult Post(string payload, string hash)
        {
            // Validate hash
            // TODO use nonce
            const String SALT = "%8vvpAwg48cvlcRwfAiY%A4gEj"; // not very secure
            var hashCompare = Crypto.GetHashString(SALT + payload);
            var response = string.Format("payload: [{0}], hash: [{1}], hashCompare: [{2}]", payload, hash, hashCompare);
            if (hashCompare.Equals(hash, StringComparison.CurrentCultureIgnoreCase))
            {
                // Map parameters to properties
                var passage = new Passage();
                switch (payload.ToUpper())
                {
                    case "IN":
                        passage.Direction = Passage.DirectionType.IN;
                        break;

                    case "OUT":
                        passage.Direction = Passage.DirectionType.OUT;
                        break;

                    default:
                        break;
                }
                passage.Message = DateTime.Now.ToString();
                CatFlapData.Save(passage);
            }
            else
            {
                return Unauthorized();
            }
            return Ok("OK");
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

//public IHttpActionResult GetCatFlap()
//{
//    Log("Reading log");
//    return Ok(File.ReadAllText(_logFile));
//}

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
