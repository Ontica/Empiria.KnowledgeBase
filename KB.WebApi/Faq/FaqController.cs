/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Frequently asked questions                   Component : Web Api                               *
*  Assembly : Empiria.KnowledgeBase.WebApi.dll             Pattern   : Controller                            *
*  Type     : FaqController                                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API controller for (frequently) asked questions.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Json;
using Empiria.WebApi;

namespace Empiria.KnowledgeBase.WebApi {

  /// <summary>Web API controller for (frequently) asked questions.</summary>
  public class FaqController : WebApiController {

    #region GET methods

    [HttpGet]
    [Route("v1/knowledge-base/faqs/{faqUID}")]
    public SingleObjectModel GetFaq(string faqUID) {
      try {

        var faq = Faq.Parse(faqUID);

        return new SingleObjectModel(this.Request, faq.ToResponse(),
                                     "FAQ");

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpGet]
    [Route("v1/knowledge-base/faqs")]
    public CollectionModel SearchFaqs([FromUri] string keywords = "") {
      try {

        FixedList<Faq> faqs = Faq.Search(keywords);

        return new CollectionModel(this.Request, faqs.ToResponse(),
                                   typeof(Faq).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    #endregion GET methods

    #region UPDATE methods

    [HttpPost]
    [Route("v1/knowledge-base/faqs")]
    public SingleObjectModel CreateFaq([FromBody] object body) {
      try {
        base.RequireBody(body);

        var bodyAsJson = JsonObject.Parse(body);

        var faq = new Faq(bodyAsJson);

        faq.Save();

        return new SingleObjectModel(this.Request, body,
                                     typeof(Faq).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v1/knowledge-base/faqs/{faqUID}")]
    public SingleObjectModel UpdateFaq(string faqUID, [FromBody] object body) {
      try {
        base.RequireBody(body);

        var bodyAsJson = JsonObject.Parse(body);

        var faq = Faq.Parse(faqUID);

        faq.Update(bodyAsJson);

        faq.Save();

        return new SingleObjectModel(this.Request, body,
                                     typeof(Faq).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion UPDATE methods

  }  // class FaqController

}  // namespace Empiria.KnowledgeBase.WebApi
