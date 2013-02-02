/*************************************************************************************************

Content List: MVC Adapter
Licensed to users of N2CMS under the terms of the Boost Software License 

Copyright (c) 2013 Benjamin Herila <mailto:ben@herila.net>

Boost Software License - Version 1.0 - August 17th, 2003

Permission is hereby granted, free of charge, to any person or organization obtaining a copy of the 
software and accompanying documentation covered by this license (the "Software") to use, reproduce,
display, distribute, execute, and transmit the Software, and to prepare derivative works of the
Software, and to permit third-parties to whom the Software is furnished to do so, all subject to 
the following:

The copyright notices in the Software and this entire statement, including the above license grant,
this restriction and the following disclaimer, must be included in all copies of the Software, in
whole or in part, and all derivative works of the Software, unless such copies or derivative works 
 are solely in the form of machine-executable object code generated by a source language processor.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-
INFRINGEMENT. IN NO EVENT SHALL THE COPYRIGHT HOLDERS OR ANYONE DISTRIBUTING THE SOFTWARE BE LIABLE
FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using N2.Engine;

namespace N2.Web.Mvc
{
	[Controls(typeof(ContentList))]
	public class ContentListController
	{
	}

	[Adapts(typeof(ContentList))]
	public class ContentListAdapter : MvcAdapter
	{
		public static string GetHtml(ContentItem model)
		{
			if (model == null)
				return ("{model = null}"); // nothing to do 
			if (!(model is ContentList))
				return ("{adapter failure - Model is not a NewsList}"); // nothing to do 

			var currentItem = model as ContentList;
			var allNews = new List<ContentItem>();
			var containerLinks = currentItem.Containers as IEnumerable<ContentListContainerLink>;
			Func<ContentItem, bool> pageCriteria = x => x.IsPage && x.Published.HasValue;
			foreach (var containerLink in containerLinks.Where(c => c.Container.IsPage))
			{
				var aChildren = containerLink.Container.GetChildren().Where(pageCriteria).ToList();  //TODO: Show unpublished news (e.g. calendar items)
				var cycleCheck = new List<ContentItem>();
				if (containerLink.Recursive)
				{
					while (aChildren.Count > 0)
					{
						var child = aChildren[0];
						aChildren.RemoveAt(0);
						var chr = child.GetChildren().Where(pageCriteria).Where(f => !cycleCheck.Contains(f));
						aChildren.AddRange(chr);
						cycleCheck.AddRange(chr);
						allNews.Add(child);
					}
				}
				else
				{
					allNews.AddRange(aChildren);
				}
			}

			var sb = new System.Text.StringBuilder(50 * 1024);

			foreach (var x in currentItem.Exceptions)
			{
				sb.AppendFormat(@"<div class=""alert alert-error""><pre>{0}</pre></div>", x);
			}

			if (!String.IsNullOrEmpty(currentItem.Title))
			{
				sb.AppendFormat("<h{0}>{1}</h{0}>", currentItem.TitleLevel, currentItem.Title);
			}

			var newsEnumerable = allNews.Where(pageCriteria);
			{
				// apply sort order ***
				if (currentItem.SortByDate == SortMode.Ascending)
					newsEnumerable = allNews.OrderBy(a => a.Published != null ? a.Published.Value : new DateTime());
				else
					newsEnumerable = allNews.OrderByDescending(a => a.Published != null ? a.Published.Value : new DateTime());

				// apply filter ***

				if (!currentItem.ShowFutureEvents)
					newsEnumerable = newsEnumerable.Where(a => a.Published != null && a.Published.Value <= DateTime.Now);

				if (!currentItem.ShowPastEvents)
					newsEnumerable = newsEnumerable.Where(a => a.Published != null && a.Published.Value >= DateTime.Now);

				if (currentItem.MaxNews > 0)
					newsEnumerable = newsEnumerable.Take(currentItem.MaxNews);
			}

			DateTime? lastDate = null;
			// why no ForEach here? it turns out that the C# compiler changed the behavior RE: how it deals with foreach
			// variables accessed from within lambda expressions. 
			using (var itemEnumerator = newsEnumerable.GetEnumerator())
				while (itemEnumerator.MoveNext())
				{
					ContentItem item = itemEnumerator.Current;
					var itemPublished = item.Published == null ? new DateTime() : item.Published.Value;
					if (currentItem.GroupByMonth && (lastDate == null || lastDate.Value.Month != itemPublished.Month))
					{
						// new month ***
						sb.AppendFormat("<h2>{0:MMMM yyyy}</h2>\n", itemPublished);
						lastDate = item.Published.Value;
					}

					var showTitle = !String.IsNullOrEmpty(item.Title);// && item.ShowTitle
					var text = (item.GetDetail("Text") ?? "").ToString();
					var summary = (item.GetDetail("Summary") ?? "").ToString();

					// display either full article or abstract + link ***
					switch (currentItem.DisplayMode)
					{
						case NewsDisplayMode.TitleAndText:
							if (showTitle)
								sb.AppendFormat("<h{1}>{0}</h{1}>\n", item.Title ?? "Untitled", currentItem.TitleLevel + 1);
							Debug.Assert(item.Published != null, "item.Published != null");
							sb.AppendFormat("<div class=\"date\">{0:MMMM d, yyyy}</div>\n", item.Published.Value);
							sb.AppendFormat("<div class=\"article\">\n{0}\n</div>\n", text);
							break;

						case NewsDisplayMode.TitleAndAbstract:
							if (!String.IsNullOrEmpty(text))
							{
								if (showTitle)
									sb.AppendFormat("<h{2}><a href=\"{1}\">{0}</a></h{2}>\n", item.Title ?? "Untitled", item.Url, currentItem.TitleLevel + 1);
								Debug.Assert(item.Published != null, "item.Published != null");
								sb.AppendFormat("<div class=\"date\">{0:MMMM d, yyyy}</div>\n", item.Published.Value);
								sb.AppendFormat("<div class=\"abstract\">\n{0}\n</div>\n", summary);
								sb.AppendFormat("<a href=\"{0}\">Read more...</a>\n", item.Url);
							}
							else
							{
								if (showTitle)
									sb.AppendFormat("<h{1}>{0}</h{1}>\n", item.Title ?? "Untitled", currentItem.TitleLevel + 1);
								Debug.Assert(item.Published != null, "item.Published != null");
								sb.AppendFormat("<div class=\"date\">{0:MMMM d, yyyy}</div>\n", item.Published.Value);
								sb.AppendFormat("<div class=\"abstract\">\n{0}\n</div>\n", summary);
							}
							break;

						case NewsDisplayMode.TitleLinkOnly:
							sb.AppendFormat("<h{2}><a href=\"{1}\">{0}</a></h{2}>\n", item.Title ?? "Untitled", item.Url, currentItem.TitleLevel + 1);
							Debug.Assert(item.Published != null, "item.Published != null");
							sb.AppendFormat("<div class=\"date\">{0:MMMM d, yyyy}</div>\n", item.Published.Value);
							break;

						case NewsDisplayMode.HtmlItemTemplate:
							if (String.IsNullOrWhiteSpace(currentItem.HtmlItemTemplate))
							{
								break;
							}
							if (!currentItem.HtmlItemTemplate.Contains('$'))
							{
								sb.Append(currentItem.HtmlItemTemplate);
								break;
							}

							sb.Append(VariableSubstituter.Substitute(currentItem.HtmlItemTemplate, item));
							break;

						default:
							Debug.Assert(false, "Assertion failed due to invalid NewsDisplayMode.");
							break;
					}
				}

			return sb.ToString();
		}

		public override void RenderTemplate(System.Web.Mvc.HtmlHelper html, ContentItem model)
		{
			html.ViewContext.Writer.Write(GetHtml(model));
		}
	}
}