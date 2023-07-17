using System;
using System.Collections.Generic;

namespace SteamNews;

public partial class Webhook
{
    public string WebhookUrl { get; set; }

    public virtual ICollection<FollowedSteamApp> FollowedSteamApps { get; set; } = new List<FollowedSteamApp>();
}
