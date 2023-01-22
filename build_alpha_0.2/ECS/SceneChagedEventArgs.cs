using System;

namespace build_alpha_0._2.ECS
{
    public class SceneChagedEventArgs : EventArgs
    {
        public uint currentSceneID { get; set; }
        public uint nextSceneID { get; set; }
    }
}
