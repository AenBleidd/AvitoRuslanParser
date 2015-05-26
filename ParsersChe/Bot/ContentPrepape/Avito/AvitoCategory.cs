﻿using ParsersChe.Bot.ActionOverPage.ContentPrepare;
using ParsersChe.Bot.ActionOverPage.EnumsPartPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParsersChe.Bot.ContentPrepape.Avito
{
  public class AvitoCategory : WebClientBot, IPrepareContent
  {
    public KeyValuePair<ActionOverPage.EnumsPartPage.PartsPage, IEnumerable<string>> RunActions(string content, string url, HtmlAgilityPack.HtmlDocument doc)
    {
      Doc = doc;
      PartsPage parts = PartsPage.Category;
      var result = GetCategoty();
      if (result != null)
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(parts, new List<string> { result });
      }
      else
      {
        return new KeyValuePair<PartsPage, IEnumerable<string>>(parts, null);
      }
    }
    private string GetCategoty()
    {
      string category = null;
      var resColl = Doc.DocumentNode.SelectNodes("//div[@class='breadcrumbs-links']/a");
      if (resColl != null && resColl.Count > 1)
      {
        category = resColl[1].InnerHtml.Trim();
      }
      return category;
    }
  }
}
