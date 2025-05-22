#include "Bitmap.h"
#define STB_IMAGE_IMPLEMENTATION
#include "../../External/stb_image.h"

Bitmap::Bitmap(std::string path) {
	m_buffer = stbi_load(path.c_str(), &m_width, &m_height, NULL, m_pixelSize);
	ConvertFormat();
}

Bitmap::~Bitmap() {
	delete m_buffer;
}

Rgba Bitmap::ReadPixel(int x, int y) const {
	int index = GetIndex(x, y);

	float b = m_buffer[index] / 255.0;
	float g = m_buffer[index + 1] / 255.0;
	float r = m_buffer[index + 2] / 255.0;
	float a = m_buffer[index + 3] / 255.0;

	return Rgba(r, g, b, a);
}

void Bitmap::WritePixel(int x, int y, Rgba rgba) {
	int index = GetIndex(x, y);

	m_buffer[index] = static_cast<PixelType>(rgba.B * 255);
	m_buffer[index + 1] = static_cast<PixelType>(rgba.G * 255);
	m_buffer[index + 2] = static_cast<PixelType>(rgba.R * 255);
	m_buffer[index + 3] = static_cast<PixelType>(rgba.A * 255);
}

size_t Bitmap::GetWidth() const noexcept {
	return m_width;
}

size_t Bitmap::GetHeight() const noexcept {
	return m_height;
}

size_t Bitmap::GetSize() const noexcept {
	return m_width * m_height * m_pixelSize;
}

PixelType* Bitmap::ToPtr() const noexcept {
	return m_buffer;
}

void Bitmap::ConvertFormat() {
	for (int y = 0; y < m_height; y++) {
		for (int x = 0; x < m_width; x++) {
			int index = GetIndex(x, y);

			char tempR = m_buffer[index];
			m_buffer[index] = m_buffer[index + 2];
			m_buffer[index + 2] = tempR;
		}
	}
}

int Bitmap::GetIndex(int x, int y) const noexcept {
	return (y * m_width + x) * m_pixelSize;
}