using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using APIBreakout.Models;

namespace APIBreakout.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PickANumber()
        {
            return View();
        }


        public ActionResult Reddit(int name)
        {
            int postnum = name;
            HttpWebRequest request = WebRequest.CreateHttp
                ("https://reddit.com/r/aww/.json");
            HttpWebResponse response =(HttpWebResponse) request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();

            //ViewBag.dat = data;

            JObject redditJson = JObject.Parse(data);

            //ViewBag.dat = redditJson["data"]["children"][0];
            List<JToken> posts = redditJson["data"]["children"].ToList();

            //JObject post = JObject.Parse(singlePost);

            List<RedditPosts> output = new List<RedditPosts>();
            //string title = singlePost["title"].ToString();


            for (int i = 0; i < postnum; i++)
            {
                RedditPosts rp = new RedditPosts();
                rp.Title = posts[i]["data"]["title"].ToString();
                rp.ImageURL= posts[i]["data"]["thumbnail"].ToString();
                rp.LinkURL = posts[i]["data"]["url"].ToString();
                output.Add(rp);
            }
            
            return View(output);
        }
    }
}