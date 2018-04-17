/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Help Desk                                    Component : Domain services                       *
*  Assembly : Empiria.KnowledgeBase.dll                    Pattern   : Power type                            *
*  Type     : TicketType                                   License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Power type used to describe a help desk ticket.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;
using Empiria.Ontology;

namespace Empiria.HelpDesk {

  /// <summary>Power type used to describe a help desk ticket.</summary>
  [Powertype(typeof(HelpDeskTicket))]
  public class TicketType : Powertype {

    #region Constructors and parsers

    private TicketType() {
      // Empiria power types always have this constructor.
    }

    static internal new TicketType Parse(int typeId) {
      return ObjectTypeInfo.Parse<TicketType>(typeId);
    }


    static public new TicketType Parse(string typeName) {
      return ObjectTypeInfo.Parse<TicketType>(typeName);
    }


    #endregion Constructors and parsers

    #region Methods

    public HelpDeskTicket CreateInstance(JsonObject data) {
      HelpDeskTicket ticket = base.CreateObject<HelpDeskTicket>();

      ticket.Update(data);

      return ticket;
    }

    #endregion Methods

  } // class TicketType

} // namespace Empiria.HelpDesk
