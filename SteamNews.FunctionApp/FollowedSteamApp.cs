using System;
using System.Collections.Generic;

namespace SteamNews;

public partial class FollowedSteamApp
{
    public int AppId { get; set; }

    public string Webhook { get; set; }

    public virtual Webhook WebhookNavigation { get; set; }
}
