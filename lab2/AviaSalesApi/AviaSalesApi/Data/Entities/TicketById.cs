﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace AviaSalesApi.Data.Entities
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class TicketById : BaseEntity
    {
        public string CountryFrom { get; set; }
        public string CityFrom { get; set; }
        public string CountryTo { get; set; }
        public string CityTo { get; set; }
        public DateTime TakeOffDay { get; set; }
        public DateTime TakeOffDate { get; set; }
        public DateTime ArriveOn { get; set; }
        public string TransitPlaces { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }

        public static TicketById From(Ticket src) => new TicketById
        {
            Id = src.Id,
            CountryFrom = src.CountryFrom,
            CityFrom = src.CityFrom,
            CountryTo = src.CountryTo,
            CityTo = src.CityTo,
            TakeOffDay = src.TakeOffDay,
            TakeOffDate = src.TakeOffDate,
            ArriveOn = src.ArriveOn,
            TransitPlaces = src.TransitPlaces,
            Company = src.Company,
            Price = src.Price
        };
    }
}