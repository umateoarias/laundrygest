package com.example.laundrygest_android.data

import android.os.Parcel
import android.os.Parcelable
import com.google.gson.annotations.SerializedName
import java.io.Serializable
import java.math.BigDecimal
import java.time.OffsetDateTime
import java.util.Date

data class CollectionDto(
    @SerializedName("number")
    val number: Int,
    @SerializedName("createdAt")
    val createdAt: String,
    @SerializedName("dueDate")
    val dueDate: String?,
    @SerializedName("taxBase")
    val taxBase: BigDecimal?,
    @SerializedName("taxAmount")
    val taxAmount: BigDecimal?,
    @SerializedName("total")
    val total: BigDecimal?,
    @SerializedName("clientCode")
    val clientCode: Int?,
    @SerializedName("collectionTypeCode")
    val collectionTypeCode: Int,
    @SerializedName("invoiceId")
    val invoiceId: Int?,
    @SerializedName("dueTotal")
    val dueTotal: BigDecimal?,
    @SerializedName("paymentMode")
    val paymentMode: String?,
    @SerializedName("collectionItems")
    val collectionItems: List<CollectionItemDto> = emptyList(),
    @SerializedName("collectionTypeCodeNavigation")
    val collectionType: CollectionTypeDto,
    @SerializedName("clientCodeNavigation")
    val client: Client
) : Serializable

data class CollectionItemDto(
    @SerializedName("id")
    val id: Int,
    @SerializedName("collectionNumber")
    val collectionNumber: Int,
    @SerializedName("collectedAt")
    val collectedAt: String?,
    @SerializedName("numPieces")
    val numPieces: Int,
    @SerializedName("pricelistCode")
    val pricelistCode: Int,
    @SerializedName("pricelistCodeNavigation")
    val pricelist: PricelistDTO
) : Serializable

data class CollectionTypeDto(
    @SerializedName("code")
    val code: Int,
    @SerializedName("name")
    val description: String,
    @SerializedName("collections")
    val collections: List<CollectionDto> = emptyList(),
    @SerializedName("pricelists")
    val pricelists: List<PricelistDTO> = emptyList()
) : Serializable

data class PricelistDTO(
    @SerializedName("code")
    val code: Int,
    @SerializedName("name")
    val name: String,
    @SerializedName("unitPrice")
    val unitPrice: BigDecimal,
    @SerializedName("collectionTypeCode")
    val collectionTypeCode: Int,
    @SerializedName("numPieces")
    val numPieces: Int,
    @SerializedName("collectionTypeCodeNavigation")
    val collectionType: CollectionTypeDto
) : Serializable
