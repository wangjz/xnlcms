using System;
using System.Collections.Generic;
using System.Web;
using COM.SingNo.Core;
using SingNoCMS.DAL;
using COM.SingNo.Common;
using SingNoCMS.XNLEngine;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
namespace SingNoCMS.Web
{
    public class ParsePage:IBasePage
    {
       private string fileExtName;
       private string pageStyle;
       private  string templatePath;
       private string charset;
       private string _path;
       private string nodeID;
       string _pagePath;
       public  ManualResetEvent allDone = new ManualResetEvent(false);
       public ParsePage()
        {
            Load += new EventHandler(Page_Load);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //TimeSpan ts1 = Process.GetCurrentProcess().TotalProcessorTime;
            //Stopwatch stw = new Stopwatch();
            //stw.Start();
            nodeID=Request.QueryString["＿nodeid"].ToString();
            _path = Request.QueryString["＿path"].ToString();
            curChannel = channelConfigManager.createInstance().channelDataColls[nodeID];
            //得到页面类型
            fileExtName = Request.QueryString["＿extname"].ToString();
            pageStyle = Request.QueryString["＿style"].ToString();
            bool isDebug = (Request.QueryString["＿debug"] == null ? true :Request.QueryString["＿debug"].ToString().ToLower().Equals("true"));
            switch (pageStyle)
            {
                case "0":
                    if (curChannel.siteNode.indexpagecreatethreadinfo.state == CreateThreadState.YES)
                    {
                        Response.Write("{\"state\":"+curChannel.siteNode.indexpagecreatethreadinfo.state.ToString()+",\"all\":"+curChannel.siteNode.indexpagecreatethreadinfo.pageCount.ToString()+",\"cur\":"+curChannel.siteNode.indexpagecreatethreadinfo.createdPageCount.ToString()+"}");
                        Response.End();
                        return;
                    }
                    break;
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    pagePath = _path;
                    break;

            }   
            templatePath = Request.QueryString["＿templatePath"].ToString();
            charset = Request.QueryString["＿charset"].ToString();
            Encoding encoder = Encoding.GetEncoding(charset);
            string[] path_array = _path.Split('/');
             _pagePath = path_array[path_array.Length - 1];   
            string templateTruePath = Server.MapPath("~" + templatePath.Replace("@", curChannel.siteNode.siteWebPath));
            string templateContent = Utils.loadTempleteByPath(templateTruePath, encoder);
            PageInfo pageinfo;
            if (curChannel.siteNode.theSiteConfig.baseConfig.AccessType == 0) //静态
            {
                switch (pageStyle)
                {
                    case "0":
                        pagePath = _path.Replace("@",curChannel.siteNode.siteWebPath)+fileExtName;
                        pageinfo = XNLParseEngine.ParseAndPageInfo(templateContent, true, this);
                        if (pageinfo.pageColls == null || pageinfo.pageColls.Count == 0)
                        {
                            createPage(pageinfo, encoder);
                            Response.Write("{\"state\":1,\"all\":1,\"cur\":1}");
                            Response.End();
                            return;
                        }
                        else if (pageinfo.pageColls != null && pageinfo.pageColls.Count > 0 && isDebug == false)
                        {
                            createPage(pageinfo, encoder);
                            Response.Write("{\"state\":" + curChannel.siteNode.indexpagecreatethreadinfo.state.ToString() + ",\"all\":" + curChannel.siteNode.indexpagecreatethreadinfo.pageCount.ToString() + ",\"cur\":" + curChannel.siteNode.indexpagecreatethreadinfo.createdPageCount.ToString() + "}");
                            Response.End();
                            return;
                        }
                        else if (pageinfo.pageColls != null && pageinfo.pageColls.Count > 0 && isDebug)
                        {
                            Response.Write("{\"state\":" + curChannel.siteNode.indexpagecreatethreadinfo.state.ToString() + ",\"all\":" + curChannel.siteNode.indexpagecreatethreadinfo.pageCount.ToString() + ",\"cur\":" + curChannel.siteNode.indexpagecreatethreadinfo.createdPageCount.ToString() + "}");
                            Response.End();
                            return;
                        }
                        //double Msecs = Process.GetCurrentProcess().TotalProcessorTime.Subtract(ts1).TotalMilliseconds;
                        //stw.Stop();
                        //HttpContext.Current.Response.Write(string.Format(" CPU时间(毫秒)={0} 实际时间(秒)={1}", Msecs, stw.Elapsed.Seconds/10, stw.ElapsedTicks));
                        //HttpContext.Current.Response.Write(string.Format("1 tick = {0}毫秒", stw.Elapsed.TotalMilliseconds / stw.Elapsed.Ticks));
                        break;
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        pagePath = _path;
                        break;

                }   
            }
        }
        private void createFile(string  parseInfo,Encoding encoder)
        {
            string createPath=Server.MapPath("~"+ pagePath.Replace("@", curChannel.siteNode.siteWebPath));
            using (FileStream fs = File.Create(createPath))
            {
                byte[] info = encoder.GetBytes(parseInfo);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }

        private void createPage(PageInfo pageinfo, Encoding encoder)
        {
            if (pageinfo.pageColls!=null&&pageinfo.pageColls.Count > 0)
            {
                //有其它页要生成
                List<int> pageList = new List<int>();
                string requestList = "";
                int i = 0;
                foreach (KeyValuePair<string, string> pinfo in pageinfo.pageColls)
                {
                    pageList.Add(Convert.ToInt32(pinfo.Value));
                    if (i == 0)
                    {
                        requestList += pinfo.Key;
                    }
                    else
                    {
                        requestList += "," + pinfo.Key;
                    }
                    i++;
                }
                List<string> pagesList = setPageNum(0, pageList.Count - 1, pageList);
                //遍历生成文件
                //if (pageList.Count > 500)
                //{
                //    ThreadPool.SetMaxThreads(256, 1000);
                //}
                curChannel.indexpagecreatethreadinfo.pageCount = pagesList.Count;
                curChannel.indexpagecreatethreadinfo.state = CreateThreadState.YES;
                //for (int k = 0; k < pagesList.Count; k++)
                //{
                //    BackgroundWorker bgWork = new BackgroundWorker();
                //    bgWork.WorkerSupportsCancellation = true;
                //    bgWork.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
                //    switch (pageStyle)
                //    {
                //        case "0":
                //            bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(indexBackgroundWorker_RunWorkerCompleted);
                //            break;
                //        case "1":
                //            bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(channelBackgroundWorker_RunWorkerCompleted);
                //            break;
                //        case "2":
                //            bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(contentBackgroundWorker_RunWorkerCompleted);
                //            break;
                //        case "3":
                //            bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(singleBackgroundWorker_RunWorkerCompleted);
                //            break;
                //    }
                //    //curChannel.indexpagecreatethreadinfo.addThread(bgWork);
                //}
                for (int k = 0; k < pagesList.Count; k++)
                {
                    string pageMsg = pagesList[k].Replace(",", "-");
                    string createPath = _path + "_" + pageMsg;
                    string tmpPageRequest = "";
                    string[] t_page = pagesList[k].Split(',');
                    string[] tn_page = requestList.Split(',');
                    int pageAddNum = 0;
                    for (var t = 0; t < t_page.Length; t++)
                    {
                        pageAddNum += Convert.ToInt32(t_page[t]) - 1;
                        tmpPageRequest += "&" + tn_page[t] + "=" + t_page[t];
                    }
                    if (pageAddNum == 0) createPath = _path;
                    ParseInfo parseinfo = new ParseInfo();
                    parseinfo.charset = charset;
                    parseinfo.createPath = Server.MapPath("~" + (createPath + fileExtName).Replace("@", curChannel.siteNode.siteWebPath));
                    parseinfo.fileExtName = fileExtName;
                    parseinfo.nodeID = nodeID;
                    parseinfo.pageNameList = requestList;
                    parseinfo.pageRequestStr = tmpPageRequest;
                    parseinfo.path = _pagePath;
                    parseinfo.templatePath = templatePath;
                    parseinfo.pageMsg = pageMsg;
                    parseinfo.pageStyle = pageStyle;
                    parseinfo.curChannel = curChannel;
                    //parseinfo.mainThread = this;
                    string hostUrl = Request.ServerVariables["HTTP_HOST"];
                    if (hostUrl.ToLower().IndexOf("http://") != 0) hostUrl = "http://" + hostUrl;
                    parseinfo.requestPagePath = hostUrl + Request.FilePath.ToLower().Replace("parse.aspx", "parseTest.aspx"); //
                    
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(new RequestParse().parse), parseinfo);
                    //allDone.WaitOne();
                    //BackgroundWorker bgWork = new BackgroundWorker();
                    //bgWork.WorkerSupportsCancellation = true;
                    //bgWork.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
                    switch (pageStyle)
                    {
                        case "0":
                            curChannel.indexpagecreatethreadinfo.threadList.Add(AbortableThreadPool.QueueUserWorkItem(new WaitCallback(RequestParse.parse), parseinfo));
                            //bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(indexBackgroundWorker_RunWorkerCompleted);
                            break;
                        case "1":
                            //bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(channelBackgroundWorker_RunWorkerCompleted);
                            break;
                        case "2":
                            //bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(contentBackgroundWorker_RunWorkerCompleted);
                            break;
                        case "3":
                            //bgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(singleBackgroundWorker_RunWorkerCompleted);
                            break;
                    }
                    //curChannel.indexpagecreatethreadinfo.addThread(bgWork);
                    //bgWork.RunWorkerAsync(parseinfo);
                    //curChannel.indexpagecreatethreadinfo.threadList[k].RunWorkerAsync(parseinfo);
                }
            }
            else  //只有一页
            {
                ParseInfo parseinfo = new ParseInfo();
                parseinfo.charset = charset;
                parseinfo.createPath = Server.MapPath("~" + (_path + fileExtName).Replace("@", curChannel.siteNode.siteWebPath));
                parseinfo.fileExtName = fileExtName;
                parseinfo.nodeID = nodeID;
                parseinfo.pageNameList = "none";
                parseinfo.pageRequestStr = "";
                parseinfo.path = _pagePath;
                parseinfo.templatePath = templatePath;
                parseinfo.pageMsg = "none";
                string hostUrl = Request.ServerVariables["HTTP_HOST"];
                if (hostUrl.ToLower().IndexOf("http://") != 0) hostUrl = "http://" + hostUrl;
                parseinfo.requestPagePath = hostUrl + Request.FilePath.ToLower().Replace("parse.aspx", "parseTest.aspx"); //
                RequestParse.parse(parseinfo);
            }
        }

        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            //BackgroundWorker worker = sender as BackgroundWorker;
            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
           // e.Result = ComputeFibonacci((int)e.Argument, worker, e);
            RequestParse.parse(e.Argument);
        }

        // This event handler deals with the results of the
        // background operation.
        private void indexBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            else if (e.Cancelled)
            {
                curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            else
            {
                curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            if (curChannel.indexpagecreatethreadinfo.pageCount == curChannel.indexpagecreatethreadinfo.createdPageCount)
            {
                //生成完成
                curChannel.indexpagecreatethreadinfo.state = CreateThreadState.NO;
                curChannel.indexpagecreatethreadinfo.pageCount = 0;
                curChannel.indexpagecreatethreadinfo.createdPageCount = 0;
                curChannel.indexpagecreatethreadinfo.threadList.Clear();
            }
        }
        private void channelBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                //curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                //curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
        }
        private void contentBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                //curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                //curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            
        }
        private void singleBackgroundWorker_RunWorkerCompleted(  object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                //curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                //curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //curChannel.indexpagecreatethreadinfo.addCreatePage();
            }
            
        }
        public void executePage(object requestPath)
        {
            try
            {
                Server.Execute(Server.UrlPathEncode(requestPath.ToString()),true);
            }
            catch
            {
            }
        }
        private List<string> setPageNum(int pageId,int maxPageId,List<int> pageList)
        {
            List<string>pagesList=new List<string>();
            if(pageId==maxPageId)
	        {
                for(int i=1;i<=pageList[pageId];i++)
                {
                    pagesList.Add(i.ToString());
                }
		        return pagesList;
	        }
	        else
	        {
		        List<string> tmp_array=setPageNum(pageId+1,maxPageId,pageList);
		        for(var i=1;i<=pageList[pageId];i++)
		        {
			        for(var j=0;j<tmp_array.Count;j++)
			        {
                        pagesList.Add(i.ToString() + "," + tmp_array[j]);
			        }
		        }
                return pagesList;
	        }
        }
    }
    class RequestParse
    {
        public static void parse(object info)
        {
            ParseInfo _info = (ParseInfo)info;
            string requestPath = _info.requestPagePath + "?＿nodeid=" + _info.nodeID + "&＿templatePath=" + _info.templatePath + "&＿charset=" + _info.charset + "&＿path=" + _info.path + "&＿extname=" + _info.fileExtName + "&＿pagemsg="+_info.pageMsg + "&＿pagename=" + _info.pageNameList + _info.pageRequestStr;
            //HttpClient parseClient = new HttpClient();
            //string responseStr= parseClient.request(requestPath);
            string responseStr = HttpClient.request(requestPath);
            Encoding encoder = Encoding.GetEncoding(_info.charset);
            using (FileStream fs = File.Create(_info.createPath))
            {
                byte[] b_info = encoder.GetBytes(responseStr);
                // Add some information to the file.
                fs.Write(b_info, 0, b_info.Length);
            }
            switch (_info.pageStyle)
            {
                case "0":
                   _info.curChannel.indexpagecreatethreadinfo.addCreatePage();
                    break;
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
            }
            if (_info.curChannel.indexpagecreatethreadinfo.pageCount == _info.curChannel.indexpagecreatethreadinfo.createdPageCount)
            {
                //生成完成
                _info.curChannel.indexpagecreatethreadinfo.state = CreateThreadState.NO;
                _info.curChannel.indexpagecreatethreadinfo.pageCount = 0;
                _info.curChannel.indexpagecreatethreadinfo.createdPageCount = 0;
                _info.curChannel.indexpagecreatethreadinfo.threadList.Clear();
            }
            //if(_info.pageNameList!="none")_info.mainThread.allDone.Set();
        }
    }
}
