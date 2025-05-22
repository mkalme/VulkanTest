#pragma once
#include <cstdint>

struct InitializeArgs {
	const char* Path;
};

struct InitializeResult {
	uint32_t Width;
	uint32_t Height;
	int ReturnCode;
};

struct ImageSourceArgs {
	uint64_t Milliseconds;
};

struct ImageSourceResult {
	uint8_t* ImageBuffer;
};