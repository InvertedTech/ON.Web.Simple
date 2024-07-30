﻿using System;

namespace SimpleWeb.Models.Subscription.Main
{
    public class PaymentViewModel
    {
        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public Guid PaymentId { get; set; }
        public string OtherPaymentId { get; set; }
        public ON.Fragments.Authorization.Payment.PaymentStatus Status { get; set; }
        public uint AmountCents { get; set; }
        public DateTime PaidOnUTC { get; set; }
        public DateTime PaidThruUTC { get; set; }

        public PaymentViewModel(ON.Fragments.Authorization.Payment.Paypal.PaypalPaymentRecord record)
        {
            if (Guid.TryParse(record.UserID, out var id))
                UserId = id;
            if (Guid.TryParse(record.SubscriptionID, out var id2))
                SubscriptionId = id2;
            if (Guid.TryParse(record.PaymentID, out var id3))
                PaymentId = id3;

            OtherPaymentId = record.PaypalPaymentID;
            Status = record.Status;
            AmountCents = record.AmountCents;

            PaidOnUTC = record.PaidOnUTC.ToDateTime();
            PaidThruUTC = record.PaidThruUTC.ToDateTime();
        }

        public string StatusPretty
        {
            get
            {
                switch (Status)
                {
                    case ON.Fragments.Authorization.Payment.PaymentStatus.PaymentComplete:
                        return "Complete";
                    case ON.Fragments.Authorization.Payment.PaymentStatus.PaymentFailed:
                        return "Failed";
                    case ON.Fragments.Authorization.Payment.PaymentStatus.PaymentPending:
                        return "Pending";
                    case ON.Fragments.Authorization.Payment.PaymentStatus.PaymentRefunded:
                        return "Refunded";
                    default:
                        return "None";
                }
            }
        }
    }
}