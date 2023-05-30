﻿using Newtonsoft.Json.Linq;
using NuGet.ContentModel;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using Smartstore.Data.Migrations;

namespace Smartstore.Core.Data.Migrations
{
    public class SmartDbContextDataSeeder : IDataSeeder<SmartDbContext>
    {
        public bool RollbackOnFailure => false;

        public async Task SeedAsync(SmartDbContext context, CancellationToken cancelToken = default)
        {
            await context.MigrateLocaleResourcesAsync(MigrateLocaleResources);
            //await MigrateSettingsAsync(context, cancelToken);
        }

        //public async Task MigrateSettingsAsync(SmartDbContext context, CancellationToken cancelToken = default)
        //{
        //    await context.SaveChangesAsync(cancelToken);
        //}

        public void MigrateLocaleResources(LocaleResourcesBuilder builder)
        {
            builder.AddOrUpdate("Account.Fields.Password", "Password", "Passwort");
            builder.AddOrUpdate("Account.Fields.PasswordSecurity", "Password security", "Passwortsicherheit");

            builder.AddOrUpdate("Account.Register.Result.AlreadyRegistered", "You are already registered.", "Sie sind bereits registriert.");
            builder.AddOrUpdate("Admin.Common.Cleanup", "Cleanup", "Aufräumen");

            builder.AddOrUpdate("Admin.System.QueuedEmails.DeleteAll.Confirm",
                "Are you sure you want to delete all sent or undeliverable emails?",
                "Sind Sie sicher, dass alle gesendeten oder unzustellbaren E-Mails gelöscht werden sollen?");

            builder.AddOrUpdate("Admin.ContentManagement.Menus.Item.InvalidTargetLink",
                "Unknown or invalid target \"{0}\" at menu link \"{1}\".",
                "Unbekanntes oder ungültiges Ziel \"{0}\" bei Menü-Link \"{1}\".");

            builder.AddOrUpdate("Account.Fields.StreetAddress2", "Street address 2", "Adresszusatz");
            builder.AddOrUpdate("Address.Fields.Address2", "Address 2", "Adresszusatz");
            builder.AddOrUpdate("Address.Fields.Address2.Required", "Address 2 is required.", "Adresszusatz wird benötigt");
            builder.AddOrUpdate("Admin.Address.Fields.Address2", "Address 2", "Adresszusatz");
            builder.AddOrUpdate("Admin.Address.Fields.Address2.Hint", "Enter address 2", "Adresszusatz bzw. zweite Adresszeile");
            builder.AddOrUpdate("Admin.Configuration.Settings.CustomerUser.StreetAddress2Enabled", "'Street address addition' enabled", "\"Adresszusatz\" aktiv");
            builder.AddOrUpdate("Admin.Configuration.Settings.CustomerUser.StreetAddress2Enabled.Hint", "Defines whether the input of 'street address addition' is enabled.", "Legt fest, ob das Eingabefeld \"Adresszusatz\" aktiviert ist");
            builder.AddOrUpdate("Admin.Configuration.Settings.CustomerUser.StreetAddress2Required", "'Street address addition' required", "\"Adresszusatz\" ist erforderlich");
            builder.AddOrUpdate("Admin.Configuration.Settings.CustomerUser.StreetAddress2Required.Hint", "Defines whether the input of 'street address addition' is required.", "Legt fest, ob die Eingabe von \"Adresszusatz\" erforderlich ist.");
            builder.AddOrUpdate("Admin.Customers.Customers.Fields.StreetAddress2", "Address 2", "Adresszusatz");
            builder.AddOrUpdate("Admin.Customers.Customers.Fields.StreetAddress2.Hint", "The address 2.", "Adresszusatz");
            builder.AddOrUpdate("Admin.Orders.Address.Address2", "Address 2", "Adresszusatz");
            builder.AddOrUpdate("PDFPackagingSlip.Address2", "Address 2: {0}", "Adresszusatz: {0}");

            builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.SocialSettings.FlickrLink",
                "Flickr Link",
                "Flickr Link",
                "Leave this field empty if the Flickr link should not be shown.",
                "Lassen Sie dieses Feld leer, wenn der Flickr Link nicht angezeigt werden soll.");

            builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.SocialSettings.LinkedInLink",
                "LinkedIn Link",
                "LinkedIn Link",
                "Leave this field empty if the LinkedIn link should not be shown.",
                "Lassen Sie dieses Feld leer, wenn der LinkedIn Link nicht angezeigt werden soll.");

            builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.SocialSettings.XingLink",
                "Xing Link",
                "Xing Link",
                "Leave this field empty if the Xing link should not be shown.",
                "Lassen Sie dieses Feld leer, wenn der Xing Link nicht angezeigt werden soll.");

            builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.SocialSettings.TikTokLink",
                "TikTok Link",
                "TikTok Link",
                "Leave this field empty if the TikTok link should not be shown.",
                "Lassen Sie dieses Feld leer, wenn der TikTok Link nicht angezeigt werden soll.");

            builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.SocialSettings.SnapchatLink",
                "Snapchat Link",
                "Snapchat Link",
                "Leave this field empty if the Snapchat link should not be shown.",
                "Lassen Sie dieses Feld leer, wenn der Snapchat Link nicht angezeigt werden soll.");

            builder.AddOrUpdate("Admin.Configuration.Settings.GeneralCommon.SocialSettings.VimeoLink",
                "Vimeo Link",
                "Vimeo Link",
                "Leave this field empty if the Vimeo link should not be shown.",
                "Lassen Sie dieses Feld leer, wenn der Vimeo Link nicht angezeigt werden soll.");
        }
    }
}