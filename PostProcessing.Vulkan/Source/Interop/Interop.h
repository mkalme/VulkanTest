#pragma once
#include "InteropStructs.hpp"

extern "C" __declspec(dllexport) InitializeResult initialize(InitializeArgs args);
extern "C" __declspec(dllexport) ImageSourceResult provideImageSource(ImageSourceArgs args);
extern "C" __declspec(dllexport) void dispose();