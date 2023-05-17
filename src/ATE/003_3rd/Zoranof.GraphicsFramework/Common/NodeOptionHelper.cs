namespace Zoranof.GraphicsFramework.Common
{
    public static class NodeOptionHelper
    {
        public static bool IsConnectOptionValid(this NodeOption to, NodeOption from) { return to.Owner != from.Owner; }
    }
}
