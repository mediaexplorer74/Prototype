/*
 * Prototype
 * Copyright (C) 2023 MediaExplorer
 */


#include "common.h"

int main(int argc, char *argv[])
{
	SDL_SetHint(SDL_HINT_WINRT_HANDLE_BACK_BUTTON, "1");

	g_argc = argc;
	g_argv = argv;

	pop_main(); // -> seg000.cpp
	
	return 0;
}

