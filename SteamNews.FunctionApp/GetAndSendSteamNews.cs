using System;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SteamNews
{
    public class GetAndSendSteamNews
    {
        private const string HARDCODED_WEBHOOK_URL = "https://discord.com/api/webhooks/1128491571294245055/aUJ892Ak3fjUdyUlI-gFp_Ell5VSQ2pe3l0krNiqtO0LB-RA60p_wdVK7gmbnil-ABPa";

        [FunctionName("GetAndSendSteamNews")]
        public void Run([TimerTrigger("%TimerTriggerTime%", RunOnStartup = true)]TimerInfo myTimer)
        {
            var webhookClient = new Discord.Webhook.DiscordWebhookClient(HARDCODED_WEBHOOK_URL);
            
            // Create and use SteamNewsDatabaseContext
            using var context = new SteamNewsDatabaseContext();
            
            // Get all followed Steam apps
            DbSet<FollowedSteamApp> followedSteamApps = context.FollowedSteamApps;
            
            // For each followed Steam app send the appid to the Discord webhook
            foreach (FollowedSteamApp followedSteamApp in followedSteamApps) webhookClient.SendMessageAsync(followedSteamApp.AppId.ToString());
        }
    }
}
