#pragma once
#include <string>
#include "Rgba.hpp"

typedef unsigned char PixelType;

class Bitmap
{
public:
	Bitmap(std::string path);
	~Bitmap();

	Rgba ReadPixel(int x, int y) const;
	void WritePixel(int x, int y, Rgba rgba);

	size_t GetWidth() const noexcept;
	size_t GetHeight() const noexcept;
	size_t GetSize() const noexcept;
	PixelType* ToPtr() const noexcept;
private:
	int m_width;
	int m_height;
	int m_pixelSize = 4;
	PixelType* m_buffer;

	void ConvertFormat();
	int GetIndex(int x, int y) const noexcept;
};