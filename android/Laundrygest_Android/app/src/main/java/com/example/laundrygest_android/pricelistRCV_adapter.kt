package com.example.laundrygest_android

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.laundrygest_android.data.PricelistDTO

class pricelistRCV_adapter(var list : List<PricelistDTO>): RecyclerView.Adapter<pricelistRCV_adapter.ViewHolder>() {
    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int
    ): ViewHolder {
        val layout = LayoutInflater.from(parent.context)
        return ViewHolder(layout.inflate(R.layout.pricelist_cardview,parent,false))
    }

    override fun onBindViewHolder(
        holder: ViewHolder,
        position: Int
    ) {
        holder.namePricelist.setText(list[position].name)
        holder.pricePricelist.setText(list[position].unitPrice.toString()+"€")
    }

    override fun getItemCount() = list.size

    class ViewHolder(val view : View) : RecyclerView.ViewHolder(view) {
        val namePricelist = view.findViewById<TextView>(R.id.pricelistName)
        val pricePricelist = view.findViewById<TextView>(R.id.pricelistPrice)
    }

    fun updateItems(newItems: List<PricelistDTO>) {
        this.list = newItems
        notifyDataSetChanged()
    }
}