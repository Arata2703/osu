using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Game.Input.Bindings;
using osu.Game.Overlays;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using osu.Framework.Utils;
using osu.Game.Beatmaps;
using osu.Game.Database;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Localisation;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Scoring;
using osu.Game.Scoring;
using osu.Game.Screens.Ranking.Statistics;
using osuTK;

namespace osu.Game.Rulesets.UI
{
    public partial class AdjustBeatmapOffset : HoldToConfirmOverlay, IKeyBindingHandler<GlobalAction>
    {

        //protected virtual bool BeatmapOffsetAdjustment => true;
        private RealmAccess realm { get; set; } = null!;

        private Task? realmWriteTask;

        private IBindable<WorkingBeatmap> beatmap { get; set; } = null!;

        public bool OnPressed(KeyBindingPressEvent<GlobalAction> e)
        {
            //if (!BeatmapOffsetAdjustment)
            //    return false;
            //Console.Write("alho poró");
            //*
            switch (e.Action)
            {
                case GlobalAction.IncreaseBeatmapOffset:
                    // Console.Write("alho poró");

                    realmWriteTask = realm.WriteAsync(r => {
                        var setInfo = r.Find<BeatmapSetInfo>(beatmap.Value.BeatmapSetInfo.ID);
                        if (setInfo == null){
                            return;
                        }
                        foreach (var b in setInfo.Beatmaps)
                        {
                            BeatmapUserSettings settings = b.UserSettings;
                            // double val = Current.Value++;
                            if(settings.Offset == 10)
                            {
                                settings.Offset+=100;
                                //System.Environment.Exit(1);
                            }
                        }
                    });
                    return true;

                case GlobalAction.DecreaseBeatmapOffset:
                    // Console.Write("alho porá");
                    realmWriteTask = realm.WriteAsync(r => {
                        var setInfo = r.Find<BeatmapSetInfo>(beatmap.Value.BeatmapSetInfo.ID);
                        if (setInfo == null){
                            return;
                        }
                        foreach (var b in setInfo.Beatmaps)
                        {
                            BeatmapUserSettings settings = b.UserSettings;
                            // double val = Current.Value++;
                            settings.Offset-=100;
                        }
                    });
                    return true;
            }
            //*/
            return false;
        } 

        public void OnReleased(KeyBindingReleaseEvent<GlobalAction> e){
        }
    }
}