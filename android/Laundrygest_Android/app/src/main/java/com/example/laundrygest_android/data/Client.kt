package com.example.laundrygest_android.data

import com.google.gson.annotations.SerializedName
import java.io.Serializable
import java.util.Date

data class Client(
    @SerializedName("code")
    val code: Int,
    @SerializedName("firstName")
    val firstName: String,
    @SerializedName("lastName")
    val lastName: String,
    @SerializedName("email")
    val email: String?,
    @SerializedName("telephone")
    val telephone: String,
    @SerializedName("nif")
    val nif: String?,
    @SerializedName("usernameApp")
    val usernameApp: String?,
    @SerializedName("passwordApp")
    val passwordApp: String?,
    @SerializedName("address")
    val address: String?,
    @SerializedName("postalCode")
    val postalCode: String?,
    @SerializedName("locality")
    val locality: String?,
    @SerializedName("lastLoginMobile")
    val lastLoginMobile: Date,
    @SerializedName("collections")
    val collections: List<CollectionDto> = emptyList()
): Serializable

