//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class PlayerEntity {

    public GameObjectComponent gameObject { get { return (GameObjectComponent)GetComponent(PlayerComponentsLookup.GameObject); } }
    public bool hasGameObject { get { return HasComponent(PlayerComponentsLookup.GameObject); } }

    public void AddGameObject(UnityEngine.GameObject newGameobject) {
        var index = PlayerComponentsLookup.GameObject;
        var component = (GameObjectComponent)CreateComponent(index, typeof(GameObjectComponent));
        component.gameobject = newGameobject;
        AddComponent(index, component);
    }

    public void ReplaceGameObject(UnityEngine.GameObject newGameobject) {
        var index = PlayerComponentsLookup.GameObject;
        var component = (GameObjectComponent)CreateComponent(index, typeof(GameObjectComponent));
        component.gameobject = newGameobject;
        ReplaceComponent(index, component);
    }

    public void RemoveGameObject() {
        RemoveComponent(PlayerComponentsLookup.GameObject);
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

    static Entitas.IMatcher<PlayerEntity> _matcherGameObject;

    public static Entitas.IMatcher<PlayerEntity> GameObject {
        get {
            if (_matcherGameObject == null) {
                var matcher = (Entitas.Matcher<PlayerEntity>)Entitas.Matcher<PlayerEntity>.AllOf(PlayerComponentsLookup.GameObject);
                matcher.componentNames = PlayerComponentsLookup.componentNames;
                _matcherGameObject = matcher;
            }

            return _matcherGameObject;
        }
    }
}