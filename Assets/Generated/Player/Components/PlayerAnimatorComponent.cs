//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class PlayerEntity {

    public AnimatorComponent animator { get { return (AnimatorComponent)GetComponent(PlayerComponentsLookup.Animator); } }
    public bool hasAnimator { get { return HasComponent(PlayerComponentsLookup.Animator); } }

    public MyService.MyEventSystem MyEventSystem
    {
        get => default;
        set
        {
        }
    }

    public MyService.PlayerViewModeController PlayerViewModeController
    {
        get => default;
        set
        {
        }
    }

    public void AddAnimator(UnityEngine.Animator newValue) {
        var index = PlayerComponentsLookup.Animator;
        var component = (AnimatorComponent)CreateComponent(index, typeof(AnimatorComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnimator(UnityEngine.Animator newValue) {
        var index = PlayerComponentsLookup.Animator;
        var component = (AnimatorComponent)CreateComponent(index, typeof(AnimatorComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnimator() {
        RemoveComponent(PlayerComponentsLookup.Animator);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class PlayerMatcher {

    static Entitas.IMatcher<PlayerEntity> _matcherAnimator;

    public static Entitas.IMatcher<PlayerEntity> Animator {
        get {
            if (_matcherAnimator == null) {
                var matcher = (Entitas.Matcher<PlayerEntity>)Entitas.Matcher<PlayerEntity>.AllOf(PlayerComponentsLookup.Animator);
                matcher.componentNames = PlayerComponentsLookup.componentNames;
                _matcherAnimator = matcher;
            }

            return _matcherAnimator;
        }
    }
}
