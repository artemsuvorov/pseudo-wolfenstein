#include <iostream>
#include <cstdio>
#include <string>
#include <algorithm>

const int textHeight = 10;

// заменяет все символы letter на newLetter
void replaceAllOccurrences(std::string* str, char letter, char newLetter) {
	std::replace(str->begin(), str->end(), letter, newLetter);

	// можно и по рабоче крестьянски циклом
	// тогда не потребуется #include <algorithm>
	/*for (size_t i = 0; i < str->length(); i++) {
	if ((*str)[i] == letter)
	(*str)[i] = newLetter;
	}*/
}

int main() {

	std::string lines[textHeight];

	printf("введите %i строк:\r\n", textHeight);
	// считываем текст из textHeight строк
	for (int i = 0; i < textHeight; i++) {
		char buffer[256];
		gets(buffer);
		lines[i] = std::string(buffer);
	}

	std::cout << std::endl << "результат: " << std::endl;
	// меняем буквы для каждой строки и выводим получившееся
	for (int i = 0; i < textHeight; i++) {
		if (i % 2 == 0)
			replaceAllOccurrences(&lines[i], 'a', 'b');
		else
			replaceAllOccurrences(&lines[i], 'b', 'a');

		printf("%s\r\n", lines[i].c_str());
	}

	return 0;
}