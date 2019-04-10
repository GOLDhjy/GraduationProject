//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class PlayerEntity {

    public PlayerAudioComponent playerAudio { get { return (PlayerAudioComponent)GetComponent(PlayerComponentsLookup.PlayerAudio); } }
    public bool hasPlayerAudio { get { return HasComponent(PlayerComponentsLookup.PlayerAudio); } }

    public void AddPlayerAudio(UnityEngine.AudioSource newValue) {
        var index = PlayerComponentsLookup.PlayerAudio;
        var component = (PlayerAudioComponent)CreateComponent(index, typeof(PlayerAudioComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePlayerAudio(UnityEngine.AudioSource newValue) {
        var index = PlayerComponentsLookup.PlayerAudio;
        var component = (PlayerAudioComponent)CreateComponent(index, typeof(PlayerAudioComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerAudio() {
        RemoveComponent(PlayerComponentsLookup.PlayerAudio);
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

    static Entitas.IMatcher<PlayerEntity> _matcherPlayerAudio;

    public static Entitas.IMatcher<PlayerEntity> PlayerAudio {
        get {
            if (_matcherPlayerAudio == null) {
                var matcher = (Entitas.Matcher<PlayerEntity>)Entitas.Matcher<PlayerEntity>.AllOf(PlayerComponentsLookup.PlayerAudio);
                matcher.componentNames = PlayerComponentsLookup.componentNames;
                _matcherPlayerAudio = matcher;
            }

            return _matcherPlayerAudio;
        }
    }
}