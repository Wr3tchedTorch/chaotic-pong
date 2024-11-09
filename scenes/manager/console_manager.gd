extends Node

signal fire_ball_effect_signal

func _ready() -> void:
	Console.pause_enabled = true
	Console.add_command("hello", console_hello, ["person_name"], 1, "Says Hello to whoever typed this command.")
	Console.add_command("fire_ball", fire_ball_effect, [], 0, "Puts the ball on fire. Someone please call the firefighters!!")	

func console_hello(param: String):
	Console.print_line("Olá " + param + ", como você está?")

func fire_ball_effect():
	Console.print_line("Tá pegando fogo bixo!")
	emit_signal("fire_ball_effect_signal")
