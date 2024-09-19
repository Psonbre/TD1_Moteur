extends CharacterBody2D

@onready var animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D
@export var speed := 10000
var idle := true
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	velocity = Input.get_vector("Left", "Right", "Up", "Down") * speed * delta;
	
	if (Input.is_action_pressed("Up")):
		animated_sprite_2d.play("Up")
		idle = false
	elif (Input.is_action_pressed("Down")):
		animated_sprite_2d.play("Down")
		idle = false
	elif (Input.is_action_pressed("Left")):
		animated_sprite_2d.play("Left")
		idle = false
	elif (Input.is_action_pressed("Right")):
		animated_sprite_2d.play("Right")
		idle = false
	elif (idle == false):
		idle = true
		animated_sprite_2d.play("Idle_"+animated_sprite_2d.animation)
	
	move_and_slide()
