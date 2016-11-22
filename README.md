

>## Changelog - Iterations

---------------

### Version 0.4 - not released

*Release date: TBD*
*Cycle length: 3 days*

- Main character sprite 2.0
- Camera follows player(s)

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


>## Completed assets list

---
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

>## GitHub stuff

------------

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

>## Discussion

-------

>As the project grew bigger we started to notice that more work had to be done just to maintain the project. Also the amoun of work that had to be done to add new features became harder and harder. The main reson for this is that all the game-systems are interconnected, and if you change one part of the game, it will effect more and more parts of the rest of the game, as the game grows. This concept is illustrated on the whiteboard below:

![Workload image](https://goo.gl/photos/oGYcVnFqjYv8QFjt7)
