package com.example.laundrygest_android.data

import com.google.gson.annotations.SerializedName
import java.util.Date

data class Client(
    @SerializedName("Code")
    val code: Int,
    @SerializedName("FirstName")
    val firstName: String,
    @SerializedName("LastName")
    val lastName: String,
    @SerializedName("Email")
    val email: String?,
    @SerializedName("Telephone")
    val telephone: String,
    @SerializedName("Nif")
    val nif: String?,
    @SerializedName("UsernameApp")
    val usernameApp: String?,
    @SerializedName("PasswordApp")
    val passwordApp: String?,
    @SerializedName("Address")
    val address: String?,
    @SerializedName("PostalCode")
    val postalCode: String?,
    @SerializedName("Locality")
    val locality: String?,
    @SerializedName("LastLoginMobile")
    val lastLoginMobile: Date,
    @SerializedName("Collections")
    val collections: List<CollectionDto> = emptyList()
)

