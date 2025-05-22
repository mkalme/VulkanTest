#pragma once
#include "Bitmap/Bitmap.h"
#include "Vulkan/VulkanInstance.hpp"
#include "Interop/InteropStructs.hpp"

class Application
{
public:
	Application(std::string path) {
		m_bitmap = new Bitmap(path);
		m_vulkanInstance = new VulkanInstance(m_bitmap);
		m_vulkanInstance->Run();
	}

	~Application() {
		delete m_vulkanInstance;
		delete m_bitmap;
	}

	ImageSourceResult ProvideImageSource(ImageSourceArgs args) const {
		ImageSourceResult output = ImageSourceResult();
		output.ImageBuffer = static_cast<uint8_t*>(m_vulkanInstance->ProvideImage());

		return output;
	}

	const Bitmap* GetBitmap() const {
		return m_bitmap;
	}
private:
	VulkanInstance* m_vulkanInstance;
	Bitmap* m_bitmap;
};