#pragma once

struct Rgba
{
public:
	float R;
	float G;
	float B;
	float A;

	constexpr Rgba() noexcept : R(0), G(0), B(0), A(0) {}
	constexpr Rgba(float r, float g, float b, float a) noexcept : R(r), G(g), B(b), A(a) {}
};