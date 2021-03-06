﻿using ParsersChe.Bot.ActionOverPage;
using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.ContentPrepare.Avito;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using ParsersChe.Bot.ContentPrepape.Avito;
using ParsersChe.WebClientParser;
using ParsersChe.WebClientParser.Proxy;
using System.Collections.Generic;

namespace AvitoRuslanParser
{
  class RuslanParser
  {
    private string pathImages = "images";
    private MySqlDB mySqlDB;
    public string PathImages
    {
      get { return pathImages; }
      set { pathImages = value; }
    }

    public RuslanParser(string user, string pass, string pathToProxy, MySqlDB _mySqlDB)
    {
      ProxyCollectionSingl.ProxyPass = pathToProxy;
      mySqlDB = _mySqlDB;
    }

    public IEnumerable<string> LoadLinks(string linkSection)
    {
      IHttpWeb webCl = new WebCl();
      string url = linkSection;
      mySqlDB.CountAd = 0;
      ParserPage parser = new SimpleParserPage
        (url, new List<IPrepareContent> {
                    new AvitoLoadLinksBeforeRepeat(webCl,8,x=>mySqlDB.IsNewAdAvito(x))
                           }, webCl
        );
      parser.LoadPage(url);
      parser.RunActions();
      var result = parser.ResultsParsing;
      var links = result[PartsPage.LinkOnAd];
      return links;

    }
    public Dictionary<PartsPage, IEnumerable<string>> Run(string link)
    {
      IHttpWeb webCl = new WebCl();
      var url = link;
      ParserPage parser = new SimpleParserPage
        (url, new List<IPrepareContent> 
                    {
                    new AvitoPhones(webCl),
                    new AvitoCity(),
                    new AvitoSeller(),
                    new AvitoTitle(),
                    new AvitoCost(),
                    new AvitoBodyAd(),
                    new AvitoSubCategory(),
                    new AvitoIdAd()
                    }, webCl
        );
      parser.LoadPage(url);
      parser.RunActions();
      //     ProxyCollectionSingl.Instance.Dispose();
      var result = parser.ResultsParsing;
      return result;
    }
  }
}
