using System.Collections;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class FireExtinguiserBuilderTests : InputTestFixture
{
    public override void Setup()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Main VR Scene.unity");
        base.Setup();
        var keyboard = InputSystem.AddDevice<Keyboard>();

        var mouse = InputSystem.AddDevice<Mouse>();
        Press(mouse.rightButton);
        Release(mouse.rightButton);
        ;
    }

    [Test]
    public void FireExtinguiserInstantiation()
    {
        GameObject fireExtinguisher = Object.Instantiate(
            Resources.Load<GameObject>("Prefabs/FireExtinguisher")
        );
        Assert.IsNotNull(fireExtinguisher);
    }

    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
