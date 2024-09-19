extends CharacterBody2D

@export var speed := 10000
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	velocity = Input.get_vector("Left", "Right", "Up", "Down") * speed * delta;
	move_and_slide()
