using ImGuiHandler;
using ImGuiNET;

namespace TutorialTestProject.Core
{
    public class TestElement : ImGuiElement
    {
        protected override void CustomRender()
        {
            ImGui.ShowDemoWindow()  ;
        }
    }
}