package com.example.laundrygest_android.tools

import java.text.SimpleDateFormat
import java.util.Date
import java.util.Locale
import kotlin.text.format

class DateParser {
    companion object {
        fun formatDate(input: String): String {
            val parser = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS", Locale.getDefault())
            val date: Date = parser.parse(input)!!

            val formatter = SimpleDateFormat("dd/MM/yyyy", Locale.getDefault())
            return formatter.format(date)
        }
    }
}