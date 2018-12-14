InputClip()
{
	Loop
	{
		Input inputText, V, {Enter}
		If (ErrorLevel = "NewInput")
			str := str inputText Clipboard
		else if (str OR inputText) {
			 break
		}
	}
	return % str inputText
}

~^vk56::Input