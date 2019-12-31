if(!$FilmModeMapped){
	$remapDivision[$remapCount] = "Film Mode";
	$remapName[$remapCount] = "Toggle Film Mode";
	$remapCmd[$remapCount] = "toggleFilmMode";
	$remapCount++;
	$FilmModeMapped = 1;
}
function ToggleFilmMode(%val){
	if(!%val)
		return;
	if($FilmMode){
		canvas.setContent(PlayGui);
	} else {
		canvas.setContent(NoHudGui);
	}
	$FilmMode = $FilmMode ? 0 : 1;
}