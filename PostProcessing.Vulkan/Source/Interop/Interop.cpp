#include "Interop.h"
#include <cstring>
#include <chrono>
#include "../Application.hpp"

static Application* App;

InitializeResult initialize(InitializeArgs args) {
	try {
		App = new Application(std::string(args.Path));
	}
	catch (const std::exception& e) {
		std::cerr << e.what() << std::endl;
		system("pause");
	}

	InitializeResult output = InitializeResult();
	output.Width = App->GetBitmap()->GetWidth();
	output.Height = App->GetBitmap()->GetHeight();
	output.ReturnCode = 0;

	return output;
}

float m_averageMilliseconds = 0;
int m_count = 0;
std::chrono::steady_clock::time_point m_last;

ImageSourceResult provideImageSource(ImageSourceArgs args) {
	auto startTime = std::chrono::high_resolution_clock::now();
	
	ImageSourceResult output =  App->ProvideImageSource(args);

	auto currentTime = std::chrono::high_resolution_clock::now();
	std::chrono::duration<float> elapsedTime = currentTime - startTime;

	m_averageMilliseconds += elapsedTime.count();
	m_count++;

	std::chrono::duration<float> timeDuration = currentTime - m_last;
	if (timeDuration.count() >= 1) {
		std::cout << std::to_string(m_averageMilliseconds / m_count * 1000) << " ms | FPS: " << (1.0 / (m_averageMilliseconds / m_count)) << "\n";

		m_averageMilliseconds = 0;
		m_count = 0;
		m_last = currentTime;
	}

	return output;
}

void dispose() {
	delete App;
}