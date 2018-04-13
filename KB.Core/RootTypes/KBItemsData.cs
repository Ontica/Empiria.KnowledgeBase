/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Knowledge Base Entities                      Component : Domain services                       *
*  Assembly : Empiria.KnowledgeBase.dll                    Pattern   : Data service                          *
*  Type     : KBItemsData                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Data read and write methods for knowledge base core entities.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.KnowledgeBase {

  /// <summary>Data read and write methods for knowledge base core entities.</summary>
  static internal class KBItemsData {

    static internal FixedList<Note> GetObjectPostingsList(string objectType, string objectUID,
                                                          string keywords = "") {
      string filter = $"(ObjectType = '{objectType}' AND ObjectUID = '{objectUID}' AND Status <> 'X')";

      if (keywords.Length != 0) {
        filter += " AND ";
        filter += SearchExpression.ParseAndLike("Keywords", EmpiriaString.BuildKeywords(keywords));
      }

      return BaseObject.GetList<Note>(filter, "PostingTime")
                       .ToFixedList();
    }

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


    static internal void WriteNote(Note o) {
      var op = DataOperation.Parse("writePosting",
                                    o.Id, o.GetEmpiriaType().Id, o.UID,
                                    o.ObjectType, o.ObjectUID,
                                    o.ControlNo, o.Title, o.Body, o.Tags,
                                    o.ExtensionData.ToString(), o.Keywords,
                                    (char) o.AccessMode, o.Owner.Id, o.ParentId,
                                    o.Date, (char) o.Status);

      DataWriter.Execute(op);
    }

  }  // class KBItemsData

}  // namespace Empiria.KnowledgeBase
