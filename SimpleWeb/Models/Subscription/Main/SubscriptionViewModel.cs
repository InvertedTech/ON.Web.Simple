﻿using ON.Fragments.Authorization.Payment.Fake;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleWeb.Models.Subscription.Main
{
    public class SubscriptionViewModel
    {
        public string Type { get; set; }
        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string OtherSubscriptionId { get; set; }
        public ON.Fragments.Authorization.Payment.SubscriptionStatus Status { get; set; }
        public uint AmountCents { get; set; }
        public DateTime StartedOnUTC { get; set; }
        public DateTime LastPaymentOnUTC { get; set; }
        public DateTime? NextPaymentOnUTC { get; set; }

        public List<PaymentViewModel> Payments { get; set; }

        public SubscriptionViewModel(ON.Fragments.Authorization.Payment.Paypal.PaypalSubscriptionFullRecord record)
        {
            Type = "Paypal";

            if (Guid.TryParse(record.Subscription.UserID, out var id))
                UserId = id;
            if (Guid.TryParse(record.Subscription.SubscriptionID, out var id2))
                SubscriptionId = id2;

            OtherSubscriptionId = record.Subscription.PaypalSubscriptionID;
            Status = record.Subscription.Status;
            AmountCents = record.Subscription.AmountCents;

            StartedOnUTC = record.Subscription.CreatedOnUTC.ToDateTime();
            LastPaymentOnUTC = record.Subscription.LastPaidUTC.ToDateTime();
            NextPaymentOnUTC = record.Subscription.RenewsOnUTC?.ToDateTime();

            Payments = record.Payments.Select(p => new PaymentViewModel(p)).ToList();
        }

        public SubscriptionViewModel(ON.Fragments.Authorization.Payment.Stripe.StripeSubscriptionFullRecord record)
        {
            Type = "Stripe";

            if (Guid.TryParse(record.Subscription.UserID, out var id))
                UserId = id;
            if (Guid.TryParse(record.Subscription.SubscriptionID, out var id2))
                SubscriptionId = id2;

            OtherSubscriptionId = record.Subscription.StripeSubscriptionID;
            Status = record.Subscription.Status;
            AmountCents = record.Subscription.AmountCents;

            StartedOnUTC = record.Subscription.CreatedOnUTC.ToDateTime();
            LastPaymentOnUTC = record.Subscription.LastPaidUTC.ToDateTime();
            NextPaymentOnUTC = record.Subscription.RenewsOnUTC?.ToDateTime();

            Payments = record.Payments.Select(p => new PaymentViewModel(p)).ToList();
        }

        public SubscriptionViewModel(FakeSubscriptionRecord record)
        {
            Type = "Fake";

            if (Guid.TryParse(record.UserID, out var id))
                UserId = id;

            OtherSubscriptionId = "Fake";
            Status = ON.Fragments.Authorization.Payment.SubscriptionStatus.SubscriptionActive;
            AmountCents = record.AmountCents;

            StartedOnUTC = record.ChangedOnUTC.ToDateTime();
            LastPaymentOnUTC = record.ChangedOnUTC.ToDateTime();
            NextPaymentOnUTC = new DateTime(2199, 12, 31, 23, 59, 59, DateTimeKind.Utc);

            Payments = new();
        }

        public string StatusPretty
        {
            get
            {
                switch (Status)
                {
                    case ON.Fragments.Authorization.Payment.SubscriptionStatus.SubscriptionActive:
                        return "Active";
                    case ON.Fragments.Authorization.Payment.SubscriptionStatus.SubscriptionPending:
                        return "Pending";
                    case ON.Fragments.Authorization.Payment.SubscriptionStatus.SubscriptionPaused:
                        return "Paused";
                    case ON.Fragments.Authorization.Payment.SubscriptionStatus.SubscriptionStopped:
                        return "Stopped";
                    default:
                        return "None";
                }
            }
        }
    }
}
