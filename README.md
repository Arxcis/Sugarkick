
## Contents

1. [Changelog - Iterations](#content1)
2. [Homegrown assets](#content2)
3. [ Git reference](#content3)
4. [Unity reference](#content4)
5. [Discussion](#content5)

&nbsp;

<a name="content1"></a>
## Changelog - Iterations


### Version 0.4 - not released

*Release date: TBD*
*Cycle length: 4 days*

- Mouse aiming
- Main character head sprite
- HUD with scoring and timer
- Fancy main menu with sound options
- Weapon pickups
- New enemy type: The Jumper
- Sugarkick mechanic - slows down time speeds up player


&nbsp;

### Version 0.3

*Release date: 21.11.2016*
*Cycle length: 4 days*

- Player can shoot bullets.
- Player can kill enemies.
- Enemy spawner can spawn/respawn enemies in different ways.
    + Wave spawn
    + Static spawn
    + Endless spawn
- Enemies falls of the map.
- Enemies can kill player.
- Player hit animation added
- Enemy killed animation added
- Holes in the map added
- All levels can be loaded from menu.
- All levels are chained together so that one level starts after the other is finished.

&nbsp;

### Version 0.2

*Released: 17. november*
*Cycle length: 6 days*

- Created an enemy spawner.
- Player can fall off the map.
- Player fall animation.
- Main menu created. Can launch the Octagon level.
- Player now has the same speed in all directions.
- Xbox 360 Controller support for Windows.


&nbsp;

### Version 0.1

*Released 11. november*
*Cycle length: 5 days*

- Basic player movement with keyboard.
- Dummy movement animation.
- Gun rotation
- A single enemy that chases the player.
- Octagon, Hexagon, Square and Triangle levels created.

&nbsp;
&nbsp;


<a name="content2"></a>
## Discussion

__Progress as a function of time__

As the project grew bigger we started to notice that more work had to be done just to maintain the project. Also the amount of work that had to be done to add new features became harder and harder. The main reson for this is that all the game-systems are interconnected, and if you change one part of the game, it will effect more and more parts of the rest of the game, as the game grows. This concept is illustrated on the whiteboard below:

<img src="images/project_1.jpg" width="60%"/>

__SCRUM - fast iterations, always finished/never finished__

The method for managing the project was chosen to be an informal type of the Scrum method.  Using GitHub extensively we were able to at least do the fast iterations part very well. GitHubs issue tracking system is also excellent for this way of project management. If implemented well into the project, the issue tracking system makes sure that every team member can find work to do in an easy way.

GitHub makes us able to work decentralized, and keeps the pace of the project at a certain speed, no matter where each team member is located. Still, it is worth mentioning that frequent meetings are needed to maintin progress speed. New issues has to be made, knowledge has to be transferred, and there is no better way than a good old fashioned meeting.


__The singleplayer -> multiplayer transition problem__

- Code written for single player is a intervined web of dependencies.
- Huge job, big risk, might be better to fork a separate repo.
- Multiplayer from the start, or no multiplayer.


&nbsp;
&nbsp;



<a name="content3"></a>
## Homegrown assets

#### Unity Prefabs

- BulletDestruction
- BulletPrefab
- BulletParent
- Camera
- Edge
- Gun
- Hole
- Particle system
- Player
- Spawner 1
- Spawner 2
- TestEnemy 1

#### Unity Animations

- EnemyDeath
- EnemyFallDown
- PlayerFallDown
- PlayerHurt
- PlayerIdle
- PlayerSprite
- PlayerWalk
- Sugarkick
- TestEnemy
- TestEnemyIdle
- TestEnemyWalk
- TestPlayer


#### Sounds

- Background music     = klar
- Meny music           = klar
- Weapon 1 sound       = klar
- Weapon 1 smash sound = klar
- Weapon 2 sound       = klar
- Weapon 2 smash sound = klar
- Press button sound   = klar
- Introduce sound      = klar ( når spilleren hopper fra menyen og inn i spillet )
- Walking sound        = klar

#### Sprites

- 2gon, 3gon, 4gon, 6gon, 8gon
- Gun
- Hole
- Main char front
- Main char back
- Player test sheet


&nbsp;
&nbsp;

<a name="content4"></a>
## Git reference

The group was motivated to use GitHub extensively from the start. 4/5 members contributed actively with each their branch, making pull requests to the master branch. 3 of the members had never used Git/GitHub before. The learning curve was steep. Below are a selection of the commands we had to learn.

> git init                                       <br />
  git clone *__url__*                            <br />
  git checkout *__branch/commit id__*            <br />
  git merge *__branch/commit id__*               <br /> <br />
  git branch                                     <br />
  git branch -m                                  <br />
  git branch -D                                  <br /> <br />
  git fetch                                      <br />
  git push origin *__branch__*                   <br />
  git pull origin *__branch__*                   <br /> <br />
  git log                                        <br />
  git log --graph                                <br />
  git log --pretty=oneline                       <br />
  git diff                                       <br /> <br />
  git remote show origin                         <br />
  git remote add *__url__*                       <br />
  git remote -v                                  <br />  
  git add .                                      <br />
  git add *__filename(s)__*                      <br />
  git commit                                     <br />
  git commit -m "*__message__*"                  <br />
  git tag                                        <br />
  git tag -a *__version__* *__commit id__*       <br />
  git tag -a *__version__* -m *__message__*      <br /> <br />
  git revert *__commit id__*                     <br />
  git reset --hard                               <br />
  git reset --hard *__tag/branch/commit id__*    <br />
  git reset HEAD~                                <br />
  git rm *__file__*                              <br />
  git mv *__filefrom fileto__*                   <br /> <br />
  git config --list                              <br />
  git config --global user.name "*__name__*"     <br />
  git config --global user.email *__email__*     <br />
  git config --global core.editor *__app-path__* --wait <br />



&nbsp;
&nbsp;

<a name="content5"></a>
## Unity reference

References to Unity's standard library assets that are used in this project

__Classes__ <br />
 >[Object](https://docs.unity3d.com/ScriptReference/Object.html) <br />
 [GameObject](https://docs.unity3d.com/ScriptReference/GameObject.html)<br />
 [Transform](https://docs.unity3d.com/ScriptReference/Transform.html)  <br />
 [Vector2](https://docs.unity3d.com/ScriptReference/Vector2.html)      <br />
 [Vector3](https://docs.unity3d.com/ScriptReference/Vector3.html)      <br />
 [Quaternion](https://docs.unity3d.com/ScriptReference/Quaternion.html) <br />
 [Rigidbody2D](https://docs.unity3d.com/ScriptReference/Rigidbody2D.html) <br/>
 [BoxCollider2D](https://docs.unity3d.com/ScriptReference/BoxCollider2D.html) <br/>
 [Sprite](https://docs.unity3d.com/ScriptReference/Sprite.html) <br/>
 [SpriteRenderer](https://docs.unity3d.com/ScriptReference/SpriteRenderer.html) <br/>
 [Material](https://docs.unity3d.com/ScriptReference/Material.html) <br/>
 [RenderTexture](https://docs.unity3d.com/ScriptReference/RenderTexture.html) <br/>
 [Shader](https://docs.unity3d.com/ScriptReference/Shader.html) <br />
 [Text](https://docs.unity3d.com/ScriptReference/UI.Text.html) <br />
 [Animator](https://docs.unity3d.com/ScriptReference/Animator.html) <br />
 [Camera](https://docs.unity3d.com/ScriptReference/Camera.html) <br/>
 [Input](https://docs.unity3d.com/ScriptReference/Input.html) <br/>
 [Debug](https://docs.unity3d.com/ScriptReference/Debug.html) <br/>

 <br />

 __Variables__ <br />
 >[Quaternion Quaternion.identity](https://docs.unity3d.com/ScriptReference/Quaternion-identity.html) <br/>
 [Transform Transform.parent](https://docs.unity3d.com/ScriptReference/Transform-parent.html) <br />
 [Sprite SpriteRenderer.sprite](https://docs.unity3d.com/ScriptReference/SpriteRenderer-sprite.html) <br />
 [Renderer.sharedMaterial](https://docs.unity3d.com/ScriptReference/Renderer-sharedMaterial.html) <br />
 [Vector3 Transform.position](https://docs.unity3d.com/ScriptReference/Transform-position.html) <br/>
 [float Vector2.magnitude](https://docs.unity3d.com/ScriptReference/Vector2-magnitude.html) <br />
 [float Camera.orthographicSize](https://docs.unity3d.com/ScriptReference/Camera-orthographicSize.html) <br/>
 [float Rigidbody2D.velocity](https://docs.unity3d.com/ScriptReference/Rigidbody2D-velocity.html) <br/>
 [float Time.deltaTime](https://docs.unity3d.com/ScriptReference/Time-deltaTime.html) <br/>
 [float Time.timeScale](https://docs.unity3d.com/ScriptReference/Time-timeScale.html) <br />
 [float Input.GetAxis](https://docs.unity3d.com/ScriptReference/Input.GetAxis.html) <br />
 [float Input.GetAxisRaw](https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html) <br />
 [Vector3 Input.mousePosition](https://docs.unity3d.com/ScriptReference/Input-mousePosition.html) <br />

 <br />

 __Functions__ <br />
 >[GameObject.CompareTag(String tag) -> bool](https://docs.unity3d.com/ScriptReference/GameObject.CompareTag.html) <br />
 [GameObject.Find(String name) -> GameObject](https://docs.unity3d.com/ScriptReference/GameObject.Find.html) <br />
 [Component.GetComponent&lt;ComponentName&gt;() -> Component](https://docs.unity3d.com/ScriptReference/Component.GetComponent.html) <br/>
 [Component.GetComponentInParent&lt;ComponentName&gt;() -> Component](https://docs.unity3d.com/ScriptReference/Component.GetComponentInParent.html) <br />
 [Component.GetComponentInChildren&lt;ComponentName&gt;() -> Component](https://docs.unity3d.com/ScriptReference/Component.GetComponentInChildren.html) <br />
 [Object.Destroy(Object)](https://docs.unity3d.com/ScriptReference/Object.Destroy.html) <br/>
 [Object.Instantiate(Object original) -> Object](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html) <br />
 [Camera.ViewportToWorldPoint(Vector3 position) -> Vector3](https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html) <br/>
 [Transform.Rotate(Vector3)](https://docs.unity3d.com/ScriptReference/Transform.Rotate.html) <br/>
 [Rigidbody2D.AddForce(Vector2)](https://docs.unity3d.com/ScriptReference/Rigidbody2D.AddForce.html) <br/>
 [Quaternion.Euler(float x, float y, float z) -> Quaternion](https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html) <br />
 [Vector3.RoateTowards -> Vector3](https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html) <br/>
 [Material.CopyPropertiesFromMaterial(Material other)](https://docs.unity3d.com/ScriptReference/Material.CopyPropertiesFromMaterial.html) <br/>
 [Material.SetTexture(string propertyName, Texture)](https://docs.unity3d.com/ScriptReference/Material.SetTexture.html) <br/>
 [Shader.Find(String name) -> Shader](https://docs.unity3d.com/ScriptReference/Shader.Find.html) <br/>
 [MonoBehaviour.Awake()](https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html) <br/>
 [MonoBehaviour.Start()](https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html) <br/>
 [MonoBehaviour.Update()](https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html) <br/>
 [MonoBehaviour.FixedUpdate()](https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html) <br/>


__Misc__

[List.add()](https://msdn.microsoft.com/en-us/library/3wcytfd1.aspx) <br />
[List.Clear()](https://msdn.microsoft.com/en-us/library/dwb5h52a.aspx) <br/>
