# **Shadow caster documentation** #

## Topics: ##
## Abstract: ##
## Prefabs: ##
the codding base is used using the concept of every interactable and not interactable object being a prefab.

### **land-prefab** ###
#### renderer order ####
the tilemap rendering order can be viwed at Land>Graphics>Grid>(Tilemap-layer)

* Hill-Walls-Tilemap
    * Represents all the object that refer as walls, is rendered with [OverCharacters](#OverCharacters) sorting layer
* Land-Bush-Tilemap
* Underground-Hill-Walls-Tilemap
* Land-Construction-Tilemap
* Land-Over-Tilemap
* Land-1-Tilemap
* Land-2-Tilemap
* Land-Underground-Tilemap


## Render Layer Organization  => ##
### **Sorting layer** ###
* #### Default ####
    * default rendering layer, every sprite here will be hendered behind everything.
* #### BehindCharacters ####
    * Every sprite rendered in this layer will be rendered behind every character in the world
    * commom layer for ground sprites.
* #### Characters ####
    * Sprite layer used to renderer every living character in the game, that includes animals, npcs, player, enemies etc...
* #### OverCharacters ####
    * Every sprite in this renderer layer will be rendered over the living characters of the game.
    * Can be used to render ground objects like walls and trees and other object that are naturally over the characters of the game
* #### OverLayers ####
    * its used the render UI effects and other effects that needs to be rendered in fron of every other sprite that is linked to the game world and not in the ui canvas
    * can be used to renderer overlays and borders of character, so that the player can see the character even if he is behind a tree or a wall
* #### Ui ####
    * every sprite that is directly linked to the ui elements of the game, like life, pause menu, and others.
* #### OverEverything ####
    * over every layer, can be used to solve bugs and other renderer related problemas and debugs.
    
