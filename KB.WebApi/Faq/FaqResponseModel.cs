/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Frequently asked questions                   Component : Web Api                               *
*  Assembly : Empiria.KnowledgeBase.WebApi.dll             Pattern   : Response methods                      *
*  Type     : FaqResponseModel                             License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Response static methods for (frequently) asked questions.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Empiria.KnowledgeBase.WebApi {

  /// <summary>Response static methods for (frequently) asked questions.</summary>
  static internal class FaqResponseModel {

    static internal ICollection ToResponse(this IList<Faq> list) {
      ArrayList array = new ArrayList(list.Count);

      foreach (var faq in list) {
        var item = faq.ToResponse();

        array.Add(item);
      }
      return array;
    }


    static internal object ToResponse(this Faq faq) {
      return new {
        uid = faq.UID,
        question = faq.Question,
        answer = faq.Answer,
        answeredBy = faq.Owner.Alias,
        comments = faq.Comments,
        date = faq.Date,
        accessMode = faq.AccessMode,
        status = faq.Status,
      };
    }

  }  // class FaqResponseModel

}  // namespace Empiria.KnowledgeBase.WebApi
