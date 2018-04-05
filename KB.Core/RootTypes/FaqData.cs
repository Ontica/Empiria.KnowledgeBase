/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Frequently asked questions                   Component : Domain services                       *
*  Assembly : Empiria.KnowledgeBase.dll                    Pattern   : Data service                          *
*  Type     : Faq                                          License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Data read and write methods for (frequently) asked questions.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.KnowledgeBase {

  /// <summary>Data read and write methods for (frequently) asked questions.</summary>
  static internal class FaqData {

    static internal FixedList<Faq> SearchFaq(string keywords = "") {
      string filter = SearchExpression.ParseAndLikeKeywords("Keywords", keywords);

      return BaseObject.GetList<Faq>(filter)
                       .ToFixedList();
    }


    static internal void WriteFaq(Faq o) {
      var op = DataOperation.Parse("writePosting",
                                    o.Id, o.GetEmpiriaType().Id, o.UID,
                                    -1, o.ControlNo,
                                    o.Question, o.Answer, o.Tags,
                                    o.Comments, o.Keywords,
                                    (char) o.AccessMode, o.Owner.Id, o.ParentId,
                                    o.Date, (char) o.Status);

      DataWriter.Execute(op);
    }

  }  // class PostingsData

}  // namespace Empiria.KnowledgeBase
